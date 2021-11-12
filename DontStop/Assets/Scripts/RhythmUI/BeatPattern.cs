using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class BeatPattern
{
    public int numMeasures;
    public List<float> notePositions;

    private int _worstNotesPerMeasure = -1;
    public float worstNotesPerMeasure()
    {
        if (_worstNotesPerMeasure == -1)
        {
            for (int i = 0; i < numMeasures; i++)
            {
                int tmp = notePositions.FindAll(p => i < p && p <= i + 1).Count;
                if (tmp > _worstNotesPerMeasure) {
                _worstNotesPerMeasure = tmp;
                }
            }
        }
        return _worstNotesPerMeasure;
    }

    public string MyToString()
    {
        string output = "";

        output += "Num Measure = " + numMeasures + "\n";

        output += "Note Positions = [";
        if(notePositions.Count == 0)
        {
            output += "]";
        }
        else
        {
            for(int i=0; i< notePositions.Count-1; i++)
            {
                output += notePositions[i] + ",";
            }
            output += notePositions[notePositions.Count - 1] + "]";
        }

        return output;
    }

    /*public BeatPattern()
    {
        numMeasures = 0;
        notePositions = new List<float>();
    }

    public int getNumMeasures()
    {
        return numMeasures;
    }

    public List<float> getNotePositions()
    {
        return new List<float>(notePositions);
    }*/
}
