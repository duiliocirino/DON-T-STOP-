using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController istance { private set; get; } = null;

    public SaveData save;

    private string saveFilePath = Application.persistentDataPath + "/save.dat";

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

        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            save = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            save = new SaveData();
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

    public void SaveRecords(int stage, int notes, int distance)
    {
        int totStages = save.levelDatas.Count;
        if (stage < 0 || stage >= totStages) return;

        save.levelDatas[stage] = new LevelData(notes, distance, save.levelDatas[stage].unlocked);
        SaveGame();
    }

    public void UnlockStage(int stage)
    {
        int totStages = save.levelDatas.Count;
        if (stage < 0 || stage >= totStages) return;

        save.levelDatas[stage].unlocked = true;
        SaveGame();
    }
}
