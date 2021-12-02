using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public Image screenBlurr;
    public Image countdown;
    public Sprite[] sprites321;
    public Image go;
    public GameObject gameOver;
    public ThirdPersonUserControl jumperControls;
    public PlayerInput creatorControls;
    public PlatformSelectionUI platformSelectionControls;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnGameStart());

        if (Options.istance.gameEnds)
        {
            LifeBar.instance.RegisterLimitReachedBehaviour(GameOver);
        }
    }

    private IEnumerator OnGameStart()
    {
        //Initialize overlay
        screenBlurr.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
        go.gameObject.SetActive(false);
        gameOver.SetActive(false);

        SetPlayerControlActive(false);

        //make music + rhythm start
        RhythmControllerUI.instance.StartNotes();

        //show tutorial
        if (Options.istance.tutorial) {
            for (int i = 1; i <= 6; i++)
            {
                TutorialController.istance.enableDialogBox("Tutorial"+i);
                yield return new WaitForSecondsRealtime(0.2f);
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && !Pause.paused);
                TutorialController.istance.disableDialogBox("Tutorial" + i);
            }
        }

        //show countdown
        for(int i=2; i>=0; i--)
        {
            countdown.sprite = sprites321[i];
            countdown.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            countdown.gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        go.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);

        go.gameObject.SetActive(false);
        screenBlurr.gameObject.SetActive(false);

        SetPlayerControlActive(true);
        TutorialController.istance.enableDialogBox("TutorialJumper");
        TutorialController.istance.enableDialogBox("TutorialCreator");

        yield return new WaitUntil(() => RhythmControllerUI.instance.noteInHitArea);
        LifeBar.instance.StartDeplition();
    }

    private void SetPlayerControlActive(bool active)
    {
        jumperControls.enabled = active;
        creatorControls.enabled = active;
        platformSelectionControls.enabled = active;
    }

    private void GameOver()
    {
        SetPlayerControlActive(false);
        screenBlurr.gameObject.SetActive(true);
        gameOver.SetActive(true);
        StartCoroutine(makeTimeStop());
    }

    private IEnumerator makeTimeStop()
    {
        float oldTimeScale = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitUntil(() => Input.anyKeyDown);
        Time.timeScale = oldTimeScale;
        SceneManager.LoadScene("StageSelection");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
