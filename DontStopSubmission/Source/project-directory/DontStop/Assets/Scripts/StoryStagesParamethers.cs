using System.Collections.Generic;

public class StoryStagesParamethers
{
    private List<List<int>> starThresholds = new List<List<int>>
    {
        new List<int>{1500, 3500, 5000}, //Tutorial
        new List<int>{2000, 5000, 10000}, //Stage 1
        new List<int>{2000, 7500, 15000}, //Stage 2
        new List<int>{2000, 7500, 15000}, //Stage 3
    };

    private List<float> songEndingOffset = new List<float>
    {
        0,
        2.50f,
        10.3f,
        2.0f,
    };

    public int getThreshold(int stage, int star)
    {
        return starThresholds[stage][star - 1];
    }

    public float getSongEndingOffset(int stage)
    {
        return songEndingOffset[stage];
    }

    //SINGLETONE CODE
    public static StoryStagesParamethers instance { get { return Nested.instance; } }

    private StoryStagesParamethers()
    {
    }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly StoryStagesParamethers instance = new StoryStagesParamethers();
    }
}
