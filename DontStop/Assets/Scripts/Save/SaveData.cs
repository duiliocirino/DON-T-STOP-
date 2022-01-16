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
            levelDatas.Add(new LevelData(0, 0, i == 0, false));
        }
    }
}

[Serializable]
public class LevelData
{
    public int noteRecord;
    public int distancerecord;
    public bool unlocked;
    public bool thingAlreadyExplained;

    public LevelData()
    {
        noteRecord = 0;
        distancerecord = 0;
        unlocked = false;
        thingAlreadyExplained = false;
    }

    public LevelData(int nR, int dR, bool u, bool a_exp)
    {
        noteRecord = nR;
        distancerecord = dR;
        unlocked = u;
        thingAlreadyExplained = a_exp;
    }
}