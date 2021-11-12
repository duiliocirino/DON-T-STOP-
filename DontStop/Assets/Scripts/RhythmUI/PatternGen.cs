using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatternGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string filename = "120MissingSecondBeat.json";

        PatternMap pm = new PatternMap();

        pm.BPM = 95;
        pm.noteSpeed = 400;
        pm.tempoNumerator = 4;
        pm.tempoDenominator = 4;
        pm.songName = "120BPMTestTrack";
        pm.initialDelay = 0;
        pm.pattern = new List<BeatPattern>();

        int trackDuration = 150;
        BeatPattern bp;
        for (int i = 0; i < trackDuration / (pm.BPM / 60); i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            //bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        File.WriteAllText("Assets/PatternMaps/" + filename, JsonUtility.ToJson(pm));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
