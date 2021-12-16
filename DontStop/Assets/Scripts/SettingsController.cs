using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public Dropdown quality;

    private SceneController sceneController;

    private void Awake()
    {
        sceneController = GetComponent<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CopySettings();
    }

    private void CopySettings()
    {
        quality.value = Settings.istance.graphicsQuality;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSettingsQuit()
    {
        SaveSettings();
        sceneController.ChangeScene("MainMenu");
    }

    private void SaveSettings()
    {
        Settings.istance.graphicsQuality = quality.value;
        QualitySettings.SetQualityLevel(quality.value);
    }
}
