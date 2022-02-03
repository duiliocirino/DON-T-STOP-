using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public List<LevelData> levelDatas;
    private int numberOfLevels = 3;

    public SaveData()
    {
        levelDatas = new List<LevelData>();

        for(int i=0; i<numberOfLevels; i++)
        {
            levelDatas.Add(new LevelData(i == 0, false));
        }
    }
}

[Serializable]
public class LevelData
{
    //public int noteRecord;
    //public int distanceRecord;

    public static readonly int maxNumRecordsSaved = 10;
    public List<LevelRecord> records;

    public bool unlocked;
    public bool thingAlreadyExplained;

    public LevelData()
    {
        //noteRecord = 0;
        //distanceRecord = 0;
        records = new List<LevelRecord>();
        unlocked = false;
        thingAlreadyExplained = false;
    }

    public bool AddRecord(int score, int distance, int notes)
    {
        int index = -1;
        for(int i=0; i<records.Count; i++)
        {
            if(score > records[i].score)
            {
                index = i;
                break;
            }
        }
        if(index == -1)
        {
            if(records.Count < maxNumRecordsSaved)
            {
                records.Add(new LevelRecord(PlayerPrefs.HasKey(PlayerSelectionController.creatorNameKey) ? PlayerPrefs.GetString(PlayerSelectionController.creatorNameKey) : "Creator",
                    PlayerPrefs.HasKey(PlayerSelectionController.runnerNameKey) ? PlayerPrefs.GetString(PlayerSelectionController.runnerNameKey) : "Runner",
                    score, distance, notes));
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            records.Insert(index, new LevelRecord(PlayerPrefs.HasKey(PlayerSelectionController.creatorNameKey) ? PlayerPrefs.GetString(PlayerSelectionController.creatorNameKey) : "Creator",
                    PlayerPrefs.HasKey(PlayerSelectionController.runnerNameKey) ? PlayerPrefs.GetString(PlayerSelectionController.runnerNameKey) : "Runner", 
                    score, distance, notes));
            while(records.Count > maxNumRecordsSaved)
            {
                records.RemoveAt(records.Count - 1);
            }
            return true;
        }
    }

    public LevelData(bool u, bool a_exp)
    {
        //noteRecord = nR;
        //distanceRecord = dR;
        records = new List<LevelRecord>();
        unlocked = u;
        thingAlreadyExplained = a_exp;
    }
}

public class LevelRecord
{
    public string creatorName;
    public string runnerName;
    public int score;
    public int distance;
    public int notes;

    public LevelRecord()
    {
        creatorName = "Creator";
        runnerName = "Runner";
        score = 0;
        distance = 0;
        notes = 0;
    }

    public LevelRecord(string creatorName, string runnerName, int score, int distance, int notes)
    {
        this.creatorName = creatorName;
        this.runnerName = runnerName;
        this.score = score;
        this.distance = distance;
        this.notes = notes;
    }
}