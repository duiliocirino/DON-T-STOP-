using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private static readonly string saveDataVersionKey = "SaveDataVersion";
    private static readonly int saveDataVersion = 1;

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
            if(!IsStoryStageUnlocked(0)) UnlockStoryStage(0);

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

            int oldSaveDataVersion = PlayerPrefs.HasKey(saveDataVersionKey) ? PlayerPrefs.GetInt(saveDataVersionKey) : 0;
            if (oldSaveDataVersion != saveDataVersion)
            {
                UpdateSaveData(oldSaveDataVersion);
            }
        }
        else
        {
            save = new SaveData(numberOfArcadeStages, numberOfStoryStages);
        }

        /* save = new SaveData(numberOfArcadeStages, numberOfStoryStages);
        UnlockStage(0);
        UnlockStage(1);
        UnlockStage(2);
        UnlockStage(3);

        UnlockStoryStage(0);
        UnlockStoryStage(1);
        UnlockStoryStage(2);
        UnlockStoryStage(3);

        CompleteStoryStage(0);
        CompleteStoryStage(1);
        CompleteStoryStage(2);
        CompleteStoryStage(3);

        SaveStoryRecords(0, 5500);
        SaveStoryRecords(1, 999999);
        SaveStoryRecords(2, 2500);
        SaveStoryRecords(3, 5500);

        SaveRecords(1, 5 * 300 + 200, 5, 200);
        SaveRecords(1, 7 * 300 + 800, 7, 800);
        SaveRecords(1, 2 * 300 + 1500, 2, 1500);

        SaveRecords(2, 4 * 300 + 300, 4, 300);
        SaveRecords(2, 9 * 300 + 900, 9, 900);
        SaveRecords(2, 7 * 300 + 1200, 7, 1200);
        SaveRecords(2, 12 * 300 + 1400, 12, 1400);

        SaveRecords(3, 7 * 300 + 250, 7, 250);
        SaveRecords(3, 22 * 300 + 1700, 22, 1700);

        SaveGame(); */
    }

    private void UpdateSaveData(int oldSaveDataVersion)
    {
        if(oldSaveDataVersion < 1)
        {
            for(int i=0; i<save.storyLevelDatas.Count-1; i++)
            {
                save.storyLevelDatas[i].completed = save.storyLevelDatas[i + 1].unlocked;
            }

            print("Save Data brought to version 1");
        }

        SaveGame();
        PlayerPrefs.SetInt(saveDataVersionKey, saveDataVersion);
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

    public void SaveStoryRecords(int stage, int score)
    {
        int totStages = save.storyLevelDatas.Count;
        if (stage < 0 || stage >= totStages) return;

        save.storyLevelDatas[stage].AddRecord(score);
        SaveGame();
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

    public void CompleteStoryStage(int stage)
    {
        int totStages = save.storyLevelDatas.Count;
        if (stage < 0 || stage >= totStages) return;

        save.storyLevelDatas[stage].completed = true;
        SaveGame();
    }

    public bool IsStoryStageCompleted(int stage)
    {
        return save.storyLevelDatas[stage].completed;
    }
}
