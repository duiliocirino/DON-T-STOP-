using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongPercentage : MonoBehaviour
{
    public AudioSource currentSong;
    public Text percentageText;

    private bool next0is100 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float perc = currentSong.time / (currentSong.clip.length - StoryStagesParamethers.instance.getSongEndingOffset(SelectedStage.istance.stageNumber));
        perc *= 100;

        if (perc > 0) next0is100 = true;
        if (perc == 0 && next0is100) perc = 100;
        if (perc > 100) perc = 100;

        int percInt = (int)perc;
        percentageText.text = percInt.ToString() + '%';
    }
}
