using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

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
    public Transform playerPosition;
    public Text distanceText;

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
        //make music + rhythm start
        RhythmControllerUI.instance.StartNotes();
        
        if (Options.istance.tutorial)
        {
            //Initialize overlay
            screenBlurr.gameObject.SetActive(true);
            countdown.gameObject.SetActive(false);
            go.gameObject.SetActive(false);
            gameOver.SetActive(false);

            //Initialise tutorial
            SetPlayerControlActive(false);
            TutorialController.instance.disableAllDialogBoxes();

            TutorialController.instance.enableDialogBox(0);
            yield return new WaitForSecondsRealtime(3f);
            TutorialController.instance.disableDialogBox(0);

            TutorialController.instance.enableDialogBox(1);
            yield return new WaitForSecondsRealtime(10f);
            TutorialController.instance.disableDialogBox(1);

            TutorialController.instance.enableDialogBox(2);
            yield return new WaitForSecondsRealtime(2f);
            SetPlayerControlActive(true);
            screenBlurr.gameObject.SetActive(false);
            yield return new WaitUntil(() =>
                (CrossPlatformInputManager.GetButtonDown("Horizontal") ||
                 CrossPlatformInputManager.GetButtonDown("Vertical"))
                && !Pause.paused);
            yield return new WaitForSecondsRealtime(2f);
            SetPlayerControlActive(false);
            TutorialController.instance.disableDialogBox(2);

            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(14);
            yield return new WaitForSecondsRealtime(2.5f);
            TutorialController.instance.disableDialogBox(14);

            TutorialController.instance.enableDialogBox(3);
            yield return new WaitForSecondsRealtime(2f);
            SetPlayerControlActive(true);
            screenBlurr.gameObject.SetActive(false);
            StartCoroutine(CheckFirstGoodJump());
            StartCoroutine(CheckFirstBadJump());
            yield return new WaitUntil(() => CrossPlatformInputManager.GetButtonDown("Jump") && !Pause.paused);
            yield return new WaitForSecondsRealtime(7f);
            SetPlayerControlActive(false);
            TutorialController.instance.disableDialogBox(3);

            TutorialController.instance.disableAllDialogBoxes();
            
            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(4);
            yield return new WaitForSecondsRealtime(8f);
            TutorialController.instance.disableDialogBox(4);

            bool platformCreated = false;
            StartCoroutine(CheckFirstGoodCreation());
            StartCoroutine(CheckFirstBadCreation());
            SetCreatorControlActive(true);
            
            while (!platformCreated)
            {
                TutorialController.instance.enableDialogBox(5);
                yield return new WaitForSecondsRealtime(0.2f);
                screenBlurr.gameObject.SetActive(false);
                yield return new WaitUntil(() =>
                    (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                     Input.GetKeyDown(KeyCode.RightArrow)) && !Pause.paused);
                TutorialController.instance.disableDialogBox(5);

                TutorialController.instance.enableDialogBox(6);
                yield return new WaitUntil(() =>
                    (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
                     Input.GetKeyUp(KeyCode.RightArrow) || Input.GetMouseButtonDown(0)) && !Pause.paused);
                if (Input.GetMouseButtonDown(0))
                {
                    platformCreated = true;
                }

                TutorialController.instance.disableDialogBox(6);
            }

            screenBlurr.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(7f);

            TutorialController.instance.enableDialogBox(13);
            yield return new WaitForSecondsRealtime(6.5f);
            TutorialController.instance.disableDialogBox(13);
            
            StartCoroutine(CheckFirstFall());
        }

        screenBlurr.gameObject.SetActive(false);
        SetPlayerControlActive(true);
        LifeBar.instance.StartDeplition();
        
        //TODO: gestire 9 e highlights

        /*//show tutorial
        if (Options.istance.tutorial) {
            
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
        */
    }

    IEnumerator CheckFirstFall()
    {
        yield return new WaitUntil(() => GameObject.FindWithTag("Player").transform.position.y < -4f && 
                                         !IsDialogActive() && !Pause.paused);
        TutorialController.instance.enableDialogBox(8);
        yield return new WaitForSecondsRealtime(3.5f);
        TutorialController.instance.disableDialogBox(8);
    }
    
    IEnumerator CheckFirstGoodJump()
    {
        yield return new WaitUntil(() => RhythmControllerUI.instance.noteInHitArea && 
                                         CrossPlatformInputManager.GetButtonDown("Jump") &&
                                         GameObject.FindWithTag("Player").GetComponent<Animator>().GetBool("OnGround") &&
                                         GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().isActiveAndEnabled &&
                                         !Pause.paused);
        TutorialController.instance.disableDialogBox(3);
        TutorialController.instance.disableDialogBox(10);
        TutorialController.instance.enableDialogBox(9);
        yield return new WaitForSecondsRealtime(6.5f);
        TutorialController.instance.disableDialogBox(9);
    }

    IEnumerator CheckFirstBadJump()
    {
        yield return new WaitUntil(() => !RhythmControllerUI.instance.noteInHitArea && 
                                         CrossPlatformInputManager.GetButtonDown("Jump") &&
                                         GameObject.FindWithTag("Player").GetComponent<Animator>().GetBool("OnGround") &&
                                         GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().isActiveAndEnabled &&
                                         !Pause.paused);
        TutorialController.instance.disableDialogBox(3);
        TutorialController.instance.disableDialogBox(9);
        TutorialController.instance.enableDialogBox(10);
        yield return new WaitForSecondsRealtime(6.5f);
        TutorialController.instance.disableDialogBox(10);
    }

    IEnumerator CheckFirstGoodCreation()
    {
        yield return new WaitUntil(() => RhythmControllerUI.instance.noteInHitArea && 
                                         (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) ||
                                          Input.GetKey(KeyCode.RightArrow)) && Input.GetMouseButtonDown(0) && 
                                         !Pause.paused);
        TutorialController.instance.disableDialogBox(6);
        TutorialController.instance.disableDialogBox(12);
        TutorialController.instance.enableDialogBox(11);
        yield return new WaitForSecondsRealtime(6.5f);
        TutorialController.instance.disableDialogBox(11);
    }

    IEnumerator CheckFirstBadCreation()
    {
        yield return new WaitUntil(() => !RhythmControllerUI.instance.noteInHitArea && 
                                         (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) ||
                                          Input.GetKey(KeyCode.RightArrow)) && Input.GetMouseButtonDown(0) && 
                                         !Pause.paused);
        TutorialController.instance.disableDialogBox(6);
        TutorialController.instance.disableDialogBox(11);
        TutorialController.instance.enableDialogBox(12);
        yield return new WaitForSecondsRealtime(6.5f);
        TutorialController.instance.disableDialogBox(12);
    }

    private bool IsDialogActive()
    {
        foreach (var dialogBox in TutorialController.instance.dialogBoxes)
        {
            if (dialogBox.activeSelf) return true;
        }
        return false;
    }

    private void SetPlayerControlActive(bool active)
    {
        jumperControls.enabled = active;
        if (active == false)
        {
            var player = GameObject.FindWithTag("Player");
            var animator = player.GetComponent<Animator>();
            animator.SetFloat("Forward", 0);
            animator.SetFloat("Turn", 0);
            animator.SetFloat("Jump", 0);
            animator.SetFloat("JumpLeg", 0);
            animator.SetBool("OnGround", true);
        }
        SetCreatorControlActive(active);
    }

    private void SetCreatorControlActive(bool active)
    {
        creatorControls.enabled = active;
        platformSelectionControls.enabled = active;
    }

    private void GameOver()
    {
        Pause.canBePaused = false;
        SetPlayerControlActive(false);
        TutorialController.instance.disableAllDialogBoxes();
        distanceText.text = "  DISTANCE REACHED: " + (playerPosition.position.z < 0 ? 0 : (int)playerPosition.position.z) + "m";
        screenBlurr.gameObject.SetActive(true);
        gameOver.SetActive(true);
        //StartCoroutine(makeTimeStop());
    }

    //private IEnumerator makeTimeStop()
    //{
    //    float oldTimeScale = Time.timeScale;
    //    Time.timeScale = 0;
    //    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
    //    Time.timeScale = oldTimeScale;
    //    SceneManager.LoadScene("MainMenu");
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
