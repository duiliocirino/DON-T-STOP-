using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool paused = false;

    public static bool canBePaused = true;

    public GameObject menu;
    public GameplayController gameplayController;
    private int stopperID = -1;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        canBePaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canBePaused)
        {
            if (paused)
            {
                OnResume();
            }
            else
            {
                OnPause();
            }
        }
    }

    private void OnPause()
    {
        menu.SetActive(true);
        stopperID = gameplayController.stopTime();
        paused = true;
    }

    public void OnResume()
    {
        gameplayController.resumeTime(stopperID);
        menu.SetActive(false);
        paused = false;
    }

    public void GoToMainMenu()
    {
        gameplayController.resumeTime(stopperID);
        RhythmControllerUI.instance.musicPlayer.Pause();
        paused = false;

        gameplayController.GoToMainMenu();
    }
}

