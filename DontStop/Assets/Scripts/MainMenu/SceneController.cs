using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ChangeScene(string sceneName)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadStage(StageButtonData stageButtonData)
    {
        if (stageButtonData.clickable)
        {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(stageButtonData.scene);
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
