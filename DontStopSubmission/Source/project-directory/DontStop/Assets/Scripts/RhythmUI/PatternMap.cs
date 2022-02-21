using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[Serializable]
public class PatternMap
{
    public string songName;
    public int tempoNumerator;
    public int tempoDenominator;
    public float BPM;
    public float noteSpeed;
    public float initialDelay;
    public int numberOfNotesSkippedOnFirstPlay;
    public List<BeatPattern> pattern;

    public PatternMap()
    {

    }
    
    public PatternMap(string s)
    {
        if (s.Equals("default120"))
        {
            BPM = 120;
            noteSpeed = 400;
            tempoNumerator = 4;
            tempoDenominator = 4;
            songName = "120BPMTestTrack";
            initialDelay = 0;
            pattern = new List<BeatPattern>();

            int trackDuration = 150;
            BeatPattern bp;
            for (int i = 0; i < trackDuration / (BPM / 60); i++)
            {
                bp = new BeatPattern();
                bp.numMeasures = 1;
                bp.notePositions = new List<float>();
                bp.notePositions.Add((float)0 / 4);
                bp.notePositions.Add((float)1 / 4);
                //bp.notePositions.Add((float)2 / 4);
                bp.notePositions.Add((float)3 / 4);
                pattern.Add(bp);
            }
        }
    }

    public string MyToString()
    {
        string output = "";

        output += "Song Name = " + songName + "\n";
        output += "Tempo = " + getTempoString() + "\n";
        output += "BPM = " + BPM + "\n";
        output += "note Speed = " + noteSpeed + "\n";

        output += "pattern = [";
        if (pattern.Count == 0)
        {
            output += "]";
        }
        else
        {
            for (int i = 0; i < pattern.Count - 1; i++)
            {
                output += pattern[i].MyToString() + ",\n";
            }
            output += pattern[pattern.Count - 1] + "]";
        }

        return output;
    }

    public string getTempoString()
    {
        return tempoNumerator + "/" + tempoDenominator;
    }

    /*private float _worstAvgNotesPerMeasure = -1;
    public float worstNotesPerMeasure()
    {
        if(_worstAvgNotesPerMeasure == -1)
        {
            _worstAvgNotesPerMeasure = pattern.Max(p => p.worstNotesPerMeasure());
        }
        return _worstAvgNotesPerMeasure;
    }

    private int _totMeasures = -1;
    public int totMeasures()
    {
        if(_totMeasures == -1)
        {
            _totMeasures = pattern.Sum(p => p.numMeasures);
        }
        return _totMeasures;
    }c#
    */
}
