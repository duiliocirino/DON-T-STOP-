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
            var videoPlayer = FindObjectOfType<VideoPlayer>();
            var music = GameObject.FindGameObjectsWithTag("MenuMusic")[0];
            Destroy(music);
            Destroy(videoPlayer);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void LoadStage(StageButtonData stageButtonData)
    {
        if (stageButtonData.clickable)
        {
            var videoPlayer = FindObjectOfType<VideoPlayer>();
            var music = GameObject.FindGameObjectsWithTag("MenuMusic")[0];
            Destroy(music);
            Destroy(videoPlayer);
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(stageButtonData.scene);
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
