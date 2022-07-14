using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryHandler : MonoBehaviour
{
    [SerializeField] GameplayController gameplayController;
    private AudioSource currentSong;
    private bool victoryTriggered;

    private void Awake()
    {
        currentSong = GetComponentInParent<AudioSource>();
    }
  
    // Update is called once per frame
    void Update()
    {
        if (currentSong.time >= currentSong.clip.length/8 && !victoryTriggered)
        {
            gameplayController.TriggerVictory();
            victoryTriggered = true;
        }
    }
}
