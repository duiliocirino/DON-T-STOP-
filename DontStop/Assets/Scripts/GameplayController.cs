using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class GameplayController : MonoBehaviour
{
    public Image screenBlurr;
    public Text countdown;
    public GameObject nextStageUnlocked;
    public GameObject gameOver;
    public ThirdPersonUserControl jumperControls;
    public PlayerInput creatorControls;
    public PlatformSelectionUI platformSelectionControls;
    public Transform playerPosition;
    public Text distanceText;
    public NotesHandler notesHandler;
    public GameObject initialPlatform;
    public GameObject lastPlatform;
    public GameObject platformChoiceUI;
    public GameObject lifebar;
    public LifeBar lifebarScript;

    private float oldTimeScale;
    private int lastTimeStopper = -1;
    private int currentTimeStopper = 0;
    private const int MAX_TIME_STOPPERS = 100;

    private void Awake()
    {
        notesHandler.onEnoughNotesCollected.Add(SaveData);
        notesHandler.onEnoughNotesCollected.Add(UnlockNextStage);

        oldTimeScale = Time.timeScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnGameStart());

        if (Options.istance.gameEnds)
        {
            LifeBar.instance.RegisterLimitReachedBehaviour(GameOver);
        }

        if (Options.istance.tutorial)
        {
            LifeBar.instance.UnregisterLimitReachedBehaviour(GameOver);
            LifeBar.instance.RegisterLimitReachedBehaviour(FirstGameOver);
        }
    }

    private IEnumerator OnGameStart()
    {
        //make music + rhythm start
        RhythmControllerUI.instance.StartNotes();

        //Initialize overlay
        screenBlurr.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
        gameOver.SetActive(false);
        SetPlayerControlActive(false);

        if (Options.istance.tutorial)
        {
            //Initialise tutorial
            platformChoiceUI.SetActive(false);
            lifebar.SetActive(false);
            lifebarScript.enabled = false;
            TutorialController.instance.disableAllDialogBoxes();

            TutorialController.instance.enableDialogBox(0);
            yield return new WaitForSecondsRealtime(2f);
            yield return new WaitUntil((() => Input.anyKeyDown && !Pause.paused));
            TutorialController.instance.disableDialogBox(0);
            
            TutorialController.instance.enableDialogBox(1);
            yield return new WaitForSecondsRealtime(4f);
            yield return new WaitUntil((() => Input.anyKeyDown && !Pause.paused));
            TutorialController.instance.disableDialogBox(1);

            TutorialController.instance.enableDialogBox(2);
            yield return new WaitForSecondsRealtime(2f);
            SetPlayerControlActive(true);
            SetCreatorControlActive(false);
            screenBlurr.gameObject.SetActive(false);
            yield return new WaitUntil(() =>
                (CrossPlatformInputManager.GetAxis("Horizontal") != 0 ||
                 CrossPlatformInputManager.GetAxis("Vertical") != 0)
                && !Pause.paused);
            yield return new WaitForSecondsRealtime(2f);
            SetPlayerControlActive(false);
            TutorialController.instance.disableDialogBox(2);

            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(15);
            yield return new WaitForSecondsRealtime(2.5f);
            TutorialController.instance.disableDialogBox(15);

            RhythmControllerUI.instance.noteInHitArea = true;
            TutorialController.instance.hitAlwaysTrue = true;
            var platform = PlaneHandler.instance.PlatformPrefabs[0];
            PlaneHandler.instance.AddPlatform(new Vector3(0,0,PlaneHandler.instance.spacing), platform);
            PlaneHandler.instance.PlatformTiles.Last().GetComponent<PlaneLogic>().planeLife = 800;
            TutorialController.instance.enableDialogBox(3);
            yield return new WaitForSecondsRealtime(2f);
            SetPlayerControlActive(true);
            SetCreatorControlActive(false);
            screenBlurr.gameObject.SetActive(false);
            bool jumpPerformed = false;
            while (!jumpPerformed)
            {
                yield return new WaitUntil(() => CrossPlatformInputManager.GetButtonDown("Jump") && !Pause.paused);
                yield return new WaitForSecondsRealtime(1f);
                if (GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().lastPlatformTouched != initialPlatform)
                {
                    jumpPerformed = true;
                    TutorialController.instance.hitAlwaysTrue = false;
                    lastPlatform = GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().lastPlatformTouched;
                    while (lastPlatform.transform.parent != null)
                    {
                        lastPlatform = lastPlatform.transform.parent.gameObject;
                    }
                }
            }
            TutorialController.instance.disableDialogBox(3);
            SetPlayerControlActive(false);
            RhythmControllerUI.instance.noteInHitArea = true;
            platform = PlaneHandler.instance.PlatformPrefabs[1];
            PlaneHandler.instance.AddPlatform(new Vector3(0,0,2 * PlaneHandler.instance.spacing), platform);
            PlaneHandler.instance.PlatformTiles.Last().GetComponent<PlaneLogic>().planeLife = 800;
            TutorialController.instance.enableDialogBox(4);
            yield return new WaitForSecondsRealtime(3f);
            yield return new WaitUntil((() => Input.anyKeyDown));
            TutorialController.instance.disableDialogBox(4);

            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(5);
            yield return MakeTimeStop();
            TutorialController.instance.disableDialogBox(5);
            screenBlurr.gameObject.SetActive(false);

            yield return new WaitUntil((() => RhythmControllerUI.instance.noteInHitArea));
            
            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(6);
            SetPlayerControlActive(true);
            SetCreatorControlActive(false);
            yield return MakeTimeStopJump();
            TutorialController.instance.disableDialogBox(6);
            screenBlurr.gameObject.SetActive(false);
            TutorialController.instance.enableDialogBox(7);

            jumpPerformed = false;
            while (!jumpPerformed)
            {
                yield return null;
                yield return new WaitUntil(() => CrossPlatformInputManager.GetButtonDown("Jump") && !Pause.paused);
                var rightJump = RhythmControllerUI.instance.noteInHitArea;
                if (!rightJump)
                {
                    StartCoroutine(Retry());
                }
                yield return new WaitForSecondsRealtime(1f);
                if (GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().lastPlatformTouched == 
                    PlaneHandler.instance.PlatformTiles[PlaneHandler.instance.PlatformTiles.Count - 1] &&
                    rightJump)
                {
                    jumpPerformed = true;
                    lastPlatform = GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().lastPlatformTouched;
                }
                if(GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().lastPlatformTouched != lastPlatform &&
                        !rightJump)
                {
                    GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().lastPlatformTouched =
                        lastPlatform;
                    GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().lastObjectPosition =
                        lastPlatform.transform.position;
                    GameObject.FindWithTag("Player").transform.position += Vector3.down*100;
                    GameObject.FindWithTag("Player").GetComponent<ThirdPersonUserControl>().HandleRespawn();
                }
            }
            TutorialController.instance.disableDialogBox(7);
            SetPlayerControlActive(false);

            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(15);
            yield return new WaitForSecondsRealtime(2.5f);
            TutorialController.instance.disableDialogBox(15);

            //CREATOR
            platformChoiceUI.SetActive(true);
            TutorialController.instance.enableDialogBox(8);
            yield return new WaitForSecondsRealtime(4f);
            yield return new WaitUntil((() => Input.anyKeyDown));
            TutorialController.instance.disableDialogBox(8);

            bool platformCreated = false;
            TutorialController.instance.hitAlwaysTrue = true;
            lastPlatform = PlaneHandler.instance.PlatformTiles[PlaneHandler.instance.PlatformTiles.Count - 1];
            SetCreatorControlActive(true);
            while (!platformCreated)
            {
                TutorialController.instance.enableDialogBox(9);
                yield return new WaitForSecondsRealtime(0.2f);
                screenBlurr.gameObject.SetActive(false);
                yield return new WaitUntil(() =>
                    (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                     Input.GetKeyDown(KeyCode.RightArrow)) && !Pause.paused);
                TutorialController.instance.disableDialogBox(9);

                TutorialController.instance.enableDialogBox(10);
                yield return new WaitUntil(() =>
                    (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
                     Input.GetKeyUp(KeyCode.RightArrow) || Input.GetMouseButtonDown(0)) && !Pause.paused);
                if (PlaneHandler.instance.PlatformTiles[PlaneHandler.instance.PlatformTiles.Count - 1] != lastPlatform)
                {
                    platformCreated = true;
                }
                TutorialController.instance.disableDialogBox(10);
            }
            TutorialController.instance.hitAlwaysTrue = false;
            SetCreatorControlActive(false);

            screenBlurr.gameObject.SetActive(true);
            // PLANE DISRUPTION
            TutorialController.instance.enableDialogBox(11);
            yield return new WaitForSecondsRealtime(1.5f);
            PlaneHandler.instance.PopPlatform();
            yield return new WaitForSecondsRealtime(1.5f);
            yield return new WaitUntil((() => Input.anyKeyDown));
            TutorialController.instance.disableDialogBox(11);
            
            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(12);
            yield return MakeTimeStop();
            TutorialController.instance.disableDialogBox(12);
            screenBlurr.gameObject.SetActive(false);
            
            yield return new WaitUntil((() => RhythmControllerUI.instance.noteInHitArea));
            
            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(13);
            SetCreatorControlActive(true);
            yield return MakeTimeStopCreate();
            TutorialController.instance.disableDialogBox(13);
            screenBlurr.gameObject.SetActive(false);
            
            SetCreatorControlActive(false);
            TutorialController.instance.enableDialogBox(14);
            yield return new WaitForSecondsRealtime(1.5f);
            PlaneHandler.instance.PopPlatform();
            SetCreatorControlActive(true);
            platformCreated = false;
            int numPlat = PlaneHandler.instance.PlatformTiles.Count;
            var rightTime = false;
            while (!platformCreated)
            {
                SetCreatorControlActive(true);
                yield return new WaitUntil(() => numPlat != PlaneHandler.instance.PlatformTiles.Count && !Pause.paused);
                SetCreatorControlActive(false);
                rightTime = RhythmControllerUI.instance.noteInHitArea;
                if (!rightTime && numPlat != PlaneHandler.instance.PlatformTiles.Count)
                {
                    PlaneHandler.instance.PopPlatform();
                    StartCoroutine(RetryCreator());
                }
                if (rightTime && numPlat < PlaneHandler.instance.PlatformTiles.Count)
                {
                    platformCreated = true;
                }
            }
            TutorialController.instance.disableDialogBox(14);
            TutorialController.instance.disableDialogBox(17);

            
            TutorialController.instance.enableDialogBox(15);
            yield return new WaitForSecondsRealtime(1.5f);
            TutorialController.instance.disableDialogBox(15);

            screenBlurr.gameObject.SetActive(true);
            TutorialController.instance.enableDialogBox(21);
            yield return new WaitForSecondsRealtime(4f);
            yield return new WaitUntil((() => Input.anyKeyDown && !Pause.paused));
            TutorialController.instance.disableDialogBox(21);
            screenBlurr.gameObject.SetActive(false);
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Stage1Scene")
            {
                screenBlurr.gameObject.SetActive(true);
                TutorialController.instance.enableDialogBox(0);
                yield return new WaitUntil((() => Input.anyKeyDown && !Pause.paused));
                TutorialController.instance.disableDialogBox(0);
                yield return new WaitForSecondsRealtime(0.5f);
                TutorialController.instance.enableDialogBox(1);
                yield return new WaitUntil((() => Input.anyKeyDown && !Pause.paused));
                TutorialController.instance.disableDialogBox(1);
                screenBlurr.gameObject.SetActive(false);

                StartCoroutine(ShowAudienceTutorial());
            }
            if (SceneManager.GetActiveScene().name == "Stage2Scene")
            {
                screenBlurr.gameObject.SetActive(true);
                TutorialController.instance.enableDialogBox(0);
                yield return new WaitUntil((() => Input.anyKeyDown && !Pause.paused));
                TutorialController.instance.disableDialogBox(0);
                screenBlurr.gameObject.SetActive(false);
                
                StartCoroutine(CheckFirstFallingPlatform());
            }
            //show countdown
            for (int i = 3; i > 0; i--)
            {
                countdown.text = i.ToString();
                countdown.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(0.5f);
                countdown.gameObject.SetActive(false);
                yield return new WaitForSecondsRealtime(0.5f);
            }

            countdown.text = "GO!";
            LifeBar.instance.StartDeplition();
            countdown.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            countdown.gameObject.SetActive(false);
        }

        TutorialController.instance.hitAlwaysTrue = false;
        screenBlurr.gameObject.SetActive(false);
        SetPlayerControlActive(true);
    }

    private IEnumerator ShowAudienceTutorial()
    {
        int numPlatforms = PlaneHandler.instance.platformTiles.Count;
        yield return new WaitUntil(() => numPlatforms < PlaneHandler.instance.platformTiles.Count);
        screenBlurr.gameObject.SetActive(true);
        TutorialController.instance.enableDialogBox(2);
        yield return MakeTimeStop();
        TutorialController.instance.disableDialogBox(2);
        screenBlurr.gameObject.SetActive(false);
    }

    private IEnumerator MakeTimeStop()
    {
        int ID = stopTime();
        yield return new WaitForSecondsRealtime(2f);
        yield return new WaitUntil((() => Input.anyKeyDown));
        resumeTime(ID);
    }
    
    private IEnumerator MakeTimeStopJump()
    {
        int ID = stopTime();
        yield return new WaitUntil((() => CrossPlatformInputManager.GetButtonDown("Jump")));
        resumeTime(ID);
    }
    
    private IEnumerator MakeTimeStopCreate()
    {
        int ID = stopTime();
        yield return new WaitUntil(() => (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) ||
                                          Input.GetKey(KeyCode.RightArrow)) && Input.GetMouseButtonDown(0) &&
                                         !Pause.paused && PlaneHandler.instance.PlatformTiles[PlaneHandler.instance.PlatformTiles.Count - 1] != lastPlatform);
        resumeTime(ID);
    }

    public int stopTime()
    {
        currentTimeStopper = (currentTimeStopper + 1) % MAX_TIME_STOPPERS;

        if (lastTimeStopper == -1)
        {
            oldTimeScale = Time.timeScale;
            RhythmControllerUI.instance.musicPlayer.Pause();
            PlatformSelectionUI.instance.ForceSelectedSlotReset();
            Time.timeScale = 0;
            lastTimeStopper = currentTimeStopper;
        }

        return currentTimeStopper;
    }

    public void resumeTime(int timeStopperID)
    {
        if (lastTimeStopper == timeStopperID)
        {
            Time.timeScale = oldTimeScale;
            RhythmControllerUI.instance.musicPlayer.Play();
            lastTimeStopper = -1;
        }
    }


    private IEnumerator CheckFirstFallingPlatform()
    {
        yield return new WaitUntil(() => TutorialController.instance.firstFall);
        screenBlurr.gameObject.SetActive(true);
        TutorialController.instance.enableDialogBox(1);
        int ID = stopTime();
        yield return new WaitForSecondsRealtime(3f);
        yield return new WaitUntil((() => Input.anyKeyDown));
        resumeTime(ID);
        screenBlurr.gameObject.SetActive(false);
        TutorialController.instance.disableDialogBox(1);
    }

    private void FirstGameOver()
    {
        StartCoroutine(NoGameOverThisTime());
    }
    private IEnumerator NoGameOverThisTime()
    {
        screenBlurr.gameObject.SetActive(true);
        TutorialController.instance.enableDialogBox(20);
        LifeBar.instance.PerfectHit();
        LifeBar.instance.PerfectHit();
        LifeBar.instance.PerfectHit();
        LifeBar.instance.PerfectHit();
        LifeBar.instance.PerfectHit();
        int ID = stopTime();
        yield return new WaitForSecondsRealtime(4);
        yield return new WaitUntil(() => Input.anyKeyDown);
        resumeTime(ID);
        screenBlurr.gameObject.SetActive(false);
        TutorialController.instance.disableDialogBox(20);
    }
    
    IEnumerator Retry()
    {
        TutorialController.instance.disableDialogBox(7);
        TutorialController.instance.enableDialogBox(16);
        yield return new WaitForSecondsRealtime(4.5f);
        TutorialController.instance.disableDialogBox(16);
    }
    
    IEnumerator RetryCreator()
    {
        TutorialController.instance.disableDialogBox(14);
        TutorialController.instance.enableDialogBox(17);    
        yield return new WaitForSecondsRealtime(4.5f);
        TutorialController.instance.disableDialogBox(17);
    }

    public void SetPlayerControlActive(bool active)
    {
        jumperControls.controlsEnabled = active;
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
        if(!active) PlatformSelectionUI.instance.ForceSelectedSlotReset();
    }

    private void GameOver()
    {
        Pause.canBePaused = false;
        SetPlayerControlActive(false);
        TutorialController.instance.disableAllDialogBoxes();
        distanceText.text = "  DISTANCE REACHED: " + DistanceReached() + "m";
        SaveData();
        screenBlurr.gameObject.SetActive(true);
        gameOver.SetActive(true);
        //StartCoroutine(makeTimeStop());
    }

    private int DistanceReached()
    {
        return playerPosition.position.z < 0 ? 0 : (int)playerPosition.position.z;
    }

    public void SaveData()
    {
#if !UNITY_EDITOR
        SaveController.istance.SaveRecords(SelectedStage.istance.stageNumber, notesHandler.notesCollected, DistanceReached());
#endif
    }

    public void EnableRealGameOver()
    {
        LifeBar.instance.RegisterLimitReachedBehaviour(GameOver);
        LifeBar.instance.UnregisterLimitReachedBehaviour(FirstGameOver);
    }

    public void UnlockNextStage()
    {
        StartCoroutine(ShowNextStageUnlocked());
#if !UNITY_EDITOR
        SaveController.istance.UnlockStage(SelectedStage.istance.stageNumber + 1);
#endif
    }

    private IEnumerator ShowNextStageUnlocked()
    {
        nextStageUnlocked.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        nextStageUnlocked.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SaveData();

        DontDestroy.created = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void GoToStageSelection()
    {
        SaveData();

        DontDestroy.created = false;

        SceneManager.LoadScene("StageSelection");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
