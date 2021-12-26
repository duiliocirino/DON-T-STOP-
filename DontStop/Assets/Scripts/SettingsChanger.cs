using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsChanger : MonoBehaviour
{
    public Dropdown quality;
    public Dropdown resolution;
    public Toggle fullScreen;
    public Slider volume;

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

        resolution.ClearOptions();
        List<string> options = new List<string>();
        Resolution[] resolutions = Settings.istance.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " +
                     resolutions[i].height;
            options.Add(option);
        }
        resolution.AddOptions(options);
        resolution.RefreshShownValue();
        resolution.value = Settings.istance.resolutionIndex;
        volume.value = Settings.istance.volume;

        fullScreen.isOn = Settings.istance.fullScreen;
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
        Settings.istance.SetGraphicsQuality(quality.value);
    }

    public void OnResolutionChange()
    {
        Settings.istance.SetResolution(resolution.value);
    }

    public void OnFullScreenChange()
    {
        Settings.istance.SetFullScreen(fullScreen.isOn);
    }

    public void OnVolumeChange()
    {
        Settings.istance.SetVolume(volume.value);
    }
}
