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
        if(sceneName == "MainMenu")
        {
            var videoPlayer = GameObject.FindGameObjectsWithTag("MenuVideo")[0];
            var music = GameObject.FindGameObjectsWithTag("MenuMusic")[0];
            Destroy(music);
            Destroy(videoPlayer);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void LoadStage()
    {
        var videoPlayer = FindObjectOfType<VideoPlayer>();
        var music = GameObject.FindGameObjectsWithTag("MenuMusic")[0];
        Destroy(music);
        Destroy(videoPlayer);
        loadingScreen.SetActive(true);
        string scene = SelectedStage.istance.selectedStage;
        Destroy(SelectedStage.istance.gameObject);
        SceneManager.LoadScene(scene);
    }

    public void LoadPlayerSelectionScreen(StageButtonData stageButtonData)
    {
        if (stageButtonData.clickable)
        {
            loadingScreen.SetActive(true);
            SelectedStage.istance.selectedStage = stageButtonData.scene;
            SceneManager.LoadScene("PlayerSelectionScene");
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
