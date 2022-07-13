using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneController : MonoBehaviour
{
    public GameObject loadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        //SaveController.istance.UnlockStage(1);
        //SaveController.istance.UnlockStage(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCancel()
    {
        Debug.Log("cancel");
    }

    public void ChangeScene(string sceneName)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadStage()
    {
        var videoPlayer = GameObject.FindGameObjectsWithTag("MenuVideo")[0];
        var music = GameObject.FindGameObjectsWithTag("MenuMusic")[0];
        Destroy(music);
        Destroy(videoPlayer);
        loadingScreen.SetActive(true);
        string scene = SelectedStage.istance.selectedStage;
        SceneManager.LoadScene(scene);
    }

    public void LoadPlayerSelectionScreen(StageButtonData stageButtonData)
    {
        loadingScreen.SetActive(true);
        SelectedStage.istance.selectedStage = stageButtonData.scene;
        SelectedStage.istance.stageNumber = stageButtonData.stageNumber;
        SelectedStage.istance.bSide = stageButtonData.bSide;
        SelectedStage.istance.story = false;
        SceneManager.LoadScene("PlayerSelectionScene");
    }

    public void LoadStoryPlayerSelectionScreen(StoryStageButtonData storyStageButtonData)
    {
        loadingScreen.SetActive(true);
        SelectedStage.istance.selectedStage = storyStageButtonData.sceneName;
        SelectedStage.istance.stageNumber = storyStageButtonData.stageNumber;
        SelectedStage.istance.bSide = false;
        SelectedStage.istance.story = true;
        SceneManager.LoadScene("PlayerSelectionScene");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
