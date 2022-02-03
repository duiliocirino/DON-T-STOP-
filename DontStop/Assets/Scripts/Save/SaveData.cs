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
            levelDatas.Add(new LevelData(0, 0, 0, i == 0, false));
        }
    }
}

[Serializable]
public class LevelData
{
    //public int noteRecord;
    //public int distanceRecord;

    private int maxNumRecordsSaved = 10;
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
                records.Add(new LevelRecord(score, distance, notes));
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            records.Insert(index, new LevelRecord(score, distance, notes));
            while(records.Count > maxNumRecordsSaved)
            {
                records.RemoveAt(records.Count - 1);
            }
            return true;
        }
    }

    public LevelData(int sR, int nR, int dR, bool u, bool a_exp)
    {
        //noteRecord = nR;
        //distanceRecord = dR;
        records = new List<LevelRecord>();
        records.Add(new LevelRecord(sR, dR, nR));
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
        creatorName = PlayerPrefs.HasKey(PlayerSelectionController.creatorNameKey) ? PlayerPrefs.GetString(PlayerSelectionController.creatorNameKey) : "Creator";
        runnerName = PlayerPrefs.HasKey(PlayerSelectionController.runnerNameKey) ? PlayerPrefs.GetString(PlayerSelectionController.runnerNameKey) : "Runner";
        score = 0;
        distance = 0;
        notes = 0;
    }

    public LevelRecord(int score, int distance, int notes)
    {
        creatorName = PlayerPrefs.HasKey(PlayerSelectionController.creatorNameKey) ? PlayerPrefs.GetString(PlayerSelectionController.creatorNameKey) : "Creator";
        runnerName = PlayerPrefs.HasKey(PlayerSelectionController.runnerNameKey) ? PlayerPrefs.GetString(PlayerSelectionController.runnerNameKey) : "Runner";
        this.score = score;
        this.distance = distance;
        this.notes = notes;
    }
}