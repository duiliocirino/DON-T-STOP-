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

    private float oldTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        canBePaused = true;
        oldTimeScale = Time.timeScale;
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
        oldTimeScale = Time.timeScale;
        RhythmControllerUI.instance.musicPlayer.Pause();
        Time.timeScale = 0f;
        paused = true;
    }

    public void OnResume()
    {
        Time.timeScale = oldTimeScale;
        RhythmControllerUI.instance.musicPlayer.Play();
        menu.SetActive(false);
        paused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = oldTimeScale;
        paused = false;

        gameplayController.SaveData();

        DontDestroy.created = false;

        SceneManager.LoadScene("MainMenu");
    }
}
