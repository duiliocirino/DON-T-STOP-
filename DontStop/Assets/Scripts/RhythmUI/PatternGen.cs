using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatternGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string filename = "Tutorial_endless.json";

        File.WriteAllText("Assets/PatternMaps/" + filename, JsonUtility.ToJson(Tutorial_endless()));
    }

    private PatternMap defaultPattern()
    {
        PatternMap pm = new PatternMap();

        pm.BPM = 95;
        pm.noteSpeed = 400;
        pm.tempoNumerator = 4;
        pm.tempoDenominator = 4;
        pm.songName = "Tutorial track";
        pm.initialDelay = 0;
        pm.numberOfNotesSkippedOnFirstPlay = 0;
        pm.pattern = new List<BeatPattern>();

        BeatPattern bp;
        float duration = 60 * 3 + 40;
        float measureTime = (60 / pm.BPM) * pm.tempoDenominator;
        int nMeasures = (int)(duration / measureTime) + 1;

        for (int i = 0; i < nMeasures; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            pm.pattern.Add(bp);
        }

        return pm;
    }

    private PatternMap Test1()
    {
        PatternMap pm = new PatternMap();

        pm.BPM = 95;
        pm.noteSpeed = 400;
        pm.tempoNumerator = 4;
        pm.tempoDenominator = 4;
        pm.songName = "120BPMTestTrack";
        pm.initialDelay = 0;
        pm.pattern = new List<BeatPattern>();

        BeatPattern bp;

        bp = new BeatPattern();
        bp.numMeasures = 2;
        bp.notePositions = new List<float>();
        pm.pattern.Add(bp);

        for (int i = 0; i < 18; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        for (int i = 0; i < 16; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            //bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        for (int i = 0; i < 8; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        for (int i = 0; i < 16; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            //bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        for (int i = 0; i < 12; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        for (int i = 0; i < 20; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            //bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        for (int i = 0; i < 20; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            //bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        for (int i = 0; i < 8; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        return pm;
    }

    private PatternMap Level2()
    {
        PatternMap pm = new PatternMap();

        pm.BPM = 100;
        pm.noteSpeed = 400;
        pm.tempoNumerator = 4;
        pm.tempoDenominator = 4;
        pm.songName = "120BPMTestTrack";
        pm.initialDelay = 0;
        pm.pattern = new List<BeatPattern>();

        BeatPattern bp;

        // riff A
        for (int i = 0; i < 2; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 2;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            bp.notePositions.Add((float)4 / 4);
            bp.notePositions.Add((float)5 / 4);
            bp.notePositions.Add((float)6 / 4);
            bp.notePositions.Add((float)7.5 / 4);
            pm.pattern.Add(bp);


            bp = new BeatPattern();
            bp.numMeasures = 2;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            //bp.notePositions.Add((float)4 / 4);
            //bp.notePositions.Add((float)5 / 4);
            bp.notePositions.Add((float)6 / 4);
            bp.notePositions.Add((float)7.5 / 4);
            pm.pattern.Add(bp);
        }
        // section B  
        for (int i = 0; i < 7; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)0.75 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)2.75 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 1;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)0.75 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)2.5 / 4);
        bp.notePositions.Add((float)3 / 4);
        bp.notePositions.Add((float)3.5 / 4);
        pm.pattern.Add(bp);

        // riff C  
        for (int i = 0; i < 2; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 2;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)1 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)3 / 4);
        bp.notePositions.Add((float)4 / 4);
        bp.notePositions.Add((float)5 / 4);
        bp.notePositions.Add((float)6 / 4);
        bp.notePositions.Add((float)7 / 4);
        pm.pattern.Add(bp);


        // section D  
        for (int i = 0; i < 7; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)2 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 1;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)0.75 / 4);
        bp.notePositions.Add((float)1.5 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)3 / 4);
        pm.pattern.Add(bp);

        // repeat riff A
        for (int i = 0; i < 2; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 2;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            bp.notePositions.Add((float)4 / 4);
            bp.notePositions.Add((float)5 / 4);
            bp.notePositions.Add((float)6 / 4);
            bp.notePositions.Add((float)7.5 / 4);
            pm.pattern.Add(bp);


            bp = new BeatPattern();
            bp.numMeasures = 2;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            //bp.notePositions.Add((float)4 / 4);
            //bp.notePositions.Add((float)5 / 4);
            bp.notePositions.Add((float)6 / 4);
            bp.notePositions.Add((float)7.5 / 4);
            pm.pattern.Add(bp);
        }


        // repeat section B  
        for (int i = 0; i < 7; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)0.75 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)2.75 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 1;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)0.75 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)2.5 / 4);
        bp.notePositions.Add((float)3 / 4);
        bp.notePositions.Add((float)3.5 / 4);
        pm.pattern.Add(bp);



        // repeat riff C  
        for (int i = 0; i < 2; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 2;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)1 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)3 / 4);
        bp.notePositions.Add((float)4 / 4);
        bp.notePositions.Add((float)5 / 4);
        bp.notePositions.Add((float)6 / 4);
        bp.notePositions.Add((float)7 / 4);
        pm.pattern.Add(bp);



        // repeat section D  
        for (int i = 0; i < 7; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)2 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 1;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)0.75 / 4);
        bp.notePositions.Add((float)1.5 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)3 / 4);
        pm.pattern.Add(bp);

        // riff E  
        for (int i = 0; i < 3; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)2 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 1;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)0.75 / 4);
        bp.notePositions.Add((float)1.5 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)3 / 4);
        pm.pattern.Add(bp);


        // repeat section D (2)  
        for (int i = 0; i < 7; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)2 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 1;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)0.75 / 4);
        bp.notePositions.Add((float)1.5 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)3 / 4);
        pm.pattern.Add(bp);


        // section F  
        for (int i = 0; i < 16; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)1.5 / 4);
            pm.pattern.Add(bp);
        }

        // repeat section B with stop 
        for (int i = 0; i < 7; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)0.75 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)2.75 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 1;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)0.75 / 4);
        bp.notePositions.Add((float)2 / 4);
        pm.pattern.Add(bp);


        // repeat section B (2)
        for (int i = 0; i < 7; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            bp.notePositions.Add((float)0.75 / 4);
            bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)2.75 / 4);
            pm.pattern.Add(bp);
        }

        bp = new BeatPattern();
        bp.numMeasures = 1;
        bp.tempoNumerator = 4;
        bp.tempoDenominator = 4;
        bp.notePositions = new List<float>();
        bp.notePositions.Add((float)0 / 4);
        bp.notePositions.Add((float)0.75 / 4);
        bp.notePositions.Add((float)2 / 4);
        bp.notePositions.Add((float)2.5 / 4);
        bp.notePositions.Add((float)3 / 4);
        bp.notePositions.Add((float)3.5 / 4);
        pm.pattern.Add(bp);



        return pm;
    }

    private PatternMap Tutorial_endless()
    {
        PatternMap pm = new PatternMap();

        pm.BPM = 85;
        pm.noteSpeed = 400;
        pm.tempoNumerator = 4;
        pm.tempoDenominator = 4;
        pm.songName = "120BPMTestTrack";
        pm.initialDelay = 0;
        pm.pattern = new List<BeatPattern>();

        BeatPattern bp;

        // pattern A
        for (int i = 0; i < 20; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            //bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            //bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        // pattern B
        for (int i = 0; i < 16; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            //bp.notePositions.Add((float)1 / 4);
            //bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        // pattern A
        for (int i = 0; i < 8; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            //bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            //bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        // pattern B
        for (int i = 0; i < 18; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            //bp.notePositions.Add((float)1 / 4);
            //bp.notePositions.Add((float)2 / 4);
            bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        // pattern C
        for (int i = 0; i < 8; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 16;
            bp.tempoDenominator = 16;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 16);
            bp.notePositions.Add((float)3 / 16);
            bp.notePositions.Add((float)6 / 16);
            bp.notePositions.Add((float)8 / 16);
            pm.pattern.Add(bp);
        }

        // pattern A
        for (int i = 0; i < 8; i++)
        {
            bp = new BeatPattern();
            bp.numMeasures = 1;
            bp.tempoNumerator = 4;
            bp.tempoDenominator = 4;
            bp.notePositions = new List<float>();
            bp.notePositions.Add((float)0 / 4);
            //bp.notePositions.Add((float)1 / 4);
            bp.notePositions.Add((float)2 / 4);
            //bp.notePositions.Add((float)3 / 4);
            pm.pattern.Add(bp);
        }

        return pm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
