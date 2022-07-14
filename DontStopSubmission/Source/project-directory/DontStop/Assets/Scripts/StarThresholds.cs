using System.Collections.Generic;

public class StarThresholds
{
    private List<List<int>> thresholds = new List<List<int>>
    {
        new List<int>{1500, 3500, 5000}, //Tutorial
        new List<int>{2000, 5000, 10000}, //Stage 1
        new List<int>{1, 2, 3}, //Stage 2
        new List<int>{2000, 5000, 10000}, //Stage 3
    };

    public int getThreshold(int stage, int star)
    {
        return thresholds[stage][star - 1];
    }

    //SINGLETONE CODE
    public static StarThresholds instance { get { return Nested.instance; } }

    private StarThresholds()
    {
    }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly StarThresholds instance = new StarThresholds();
    }
}
