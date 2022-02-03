using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionController : MonoBehaviour
{
    public SceneController sceneController;
    public InputField creatorName;
    public InputField runnerName;

    public static string creatorNameKey = "CreatorName";
    public static string runnerNameKey = "RunnerName";

    public void Awake()
    {
        if (PlayerPrefs.HasKey(runnerNameKey))
        {
            runnerName.text = PlayerPrefs.GetString(runnerNameKey);
        }
        else
        {
            runnerName.text = "Runner";
        }

        if (PlayerPrefs.HasKey(creatorNameKey))
        {
            creatorName.text = PlayerPrefs.GetString(creatorNameKey);
        }
        else
        {
            creatorName.text = "Creator";
        }
    }

    public void OnStart()
    {
        PlayerPrefs.SetString(creatorNameKey, creatorName.text);
        PlayerPrefs.SetString(runnerNameKey, runnerName.text);
        PlayerPrefs.Save();
        sceneController.LoadStage();
    }
}
