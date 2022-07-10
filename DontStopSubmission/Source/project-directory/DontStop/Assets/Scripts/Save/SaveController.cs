using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController istance { private set; get; } = null;

    private SaveData save;

    private string saveFilePath;

    [SerializeField]
    private int numberOfArcadeStages = 4;
    [SerializeField]
    private int numberOfStoryStages = 4;

    private void Awake()
    {
        if (istance == null)
        {
            istance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        saveFilePath = Application.persistentDataPath + "/save.dat";
        //Debug.Log(saveFilePath);

        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            save = (SaveData)bf.Deserialize(file);

            while(save.levelDatas.Count < numberOfArcadeStages)
            {
                save.levelDatas.Add(new LevelData());
            }

            if (save.storyLevelDatas == null) save.storyLevelDatas = new List<StoryLevelData>();
            while (save.storyLevelDatas.Count < numberOfStoryStages)
            {
                save.storyLevelDatas.Add(new StoryLevelData());
            }

            //necessary for backwards compatibility
            foreach (LevelData levelData in save.levelDatas)
            {
                if (levelData.records == null) levelData.records = new List<LevelRecord>();
            }

            foreach (StoryLevelData levelData in save.storyLevelDatas)
            {
                if (levelData.records == null) levelData.records = new List<StoryLevelRecord>();
            }

            file.Close();
        }
        else
        {
            save = new SaveData(numberOfArcadeStages, numberOfStoryStages);
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void SaveRecords(int stage, int score, int notes, int distance)
    {
        int totStages = save.levelDatas.Count;
        if (stage < 0 || stage >= totStages) return;

        save.levelDatas[stage].AddRecord(score, distance, notes);
        SaveGame();
    }

    public List<LevelRecord> GetRecords(int stage)
    {
        int totStages = save.levelDatas.Count;
        if (stage < 0 || stage >= totStages) return null;

        return save.levelDatas[stage].records;
    }

    public int GetHighestScoreRecord(int stage)
    {
        int totStages = save.levelDatas.Count;
        if (stage < 0 || stage >= totStages) return 0;

        if (save.levelDatas[stage].records.Count <= 0) return 0;

        return save.levelDatas[stage].records[0].score;
    }

    public void UnlockStage(int stage)
    {
        int totStages = save.levelDatas.Count;
        if (stage < 0 || stage >= totStages) return;

        save.levelDatas[stage].unlocked = true;
        SaveGame();
    }

    public bool IsStageUnlocked(int stage)
    {
        return save.levelDatas[stage].unlocked;
    }

    public bool ThingAlreadyExplained(int stage)
    {
        int totStages = save.levelDatas.Count;
        if (stage < 0 || stage >= totStages) throw new System.IndexOutOfRangeException();

        LevelData ld = save.levelDatas[stage];
        if(!ld.thingAlreadyExplained)
        {
            ld.thingAlreadyExplained = true;
            SaveGame();
            return false;
        }
        return true;
    }

    public List<StoryLevelRecord> GetStoryRecords(int stage)
    {
        int totStages = save.storyLevelDatas.Count;
        if (stage < 0 || stage >= totStages) return null;

        return save.storyLevelDatas[stage].records;
    }

    public int GetHighestStoryScoreRecord(int stage)
    {
        int totStages = save.storyLevelDatas.Count;
        if (stage < 0 || stage >= totStages) return 0;

        if (save.storyLevelDatas[stage].records.Count <= 0) return 0;

        return save.storyLevelDatas[stage].records[0].score;
    }

    public void UnlockStoryStage(int stage)
    {
        int totStages = save.storyLevelDatas.Count;
        if (stage < 0 || stage >= totStages) return;

        save.storyLevelDatas[stage].unlocked = true;
        SaveGame();
    }

    public bool IsStoryStageUnlocked(int stage)
    {
        return save.storyLevelDatas[stage].unlocked;
    }
}
