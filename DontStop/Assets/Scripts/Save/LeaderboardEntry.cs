using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField]
    private Text index;

    [SerializeField]
    private Text creatorName;

    [SerializeField]
    private Text runnerName;

    [SerializeField]
    private Text score;

    [SerializeField]
    private Text distance;

    [SerializeField]
    private Text notes;

    public void UpdateContent(int index, LevelRecord data)
    {
        this.index.text = index.ToString();
        creatorName.text = data.creatorName;
        runnerName.text = data.runnerName;
        score.text = data.score.ToString();
        distance.text = data.distance.ToString();
        notes.text = data.notes.ToString();
    }

    public List<string> GetContent()
    {
        List<string> ret = new List<string>(6);

        ret.Add(index.text);
        ret.Add(creatorName.text);
        ret.Add(runnerName.text);
        ret.Add(score.text);
        ret.Add(distance.text);
        ret.Add(notes.text);

        return ret;
    }
}
