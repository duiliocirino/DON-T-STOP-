using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsChanger : MonoBehaviour
{
    public GameObject mainSettings;
    public GameObject calibrationSettings;

    public Dropdown quality;
    public Dropdown resolution;
    public Toggle fullScreen;
    public Slider volume;

    public Slider calibration;
    public Text calibrationText;
    public RhythmControllerUI notesUIScript;
    private float lastAppliedCalibration;

    private AudioSource menuMusic;

    private SceneController sceneController;

    private void Awake()
    {
        sceneController = GetComponent<SceneController>();
        menuMusic = GameObject.FindGameObjectsWithTag("MenuMusic")[0].GetComponent<AudioSource>();
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

        SetCalibration(Settings.istance.notesLatencyOffset * 1000);
        lastAppliedCalibration = Settings.istance.notesLatencyOffset * 1000;
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

    private void SetCalibration(float calibration)
    {
        this.calibration.value = calibration;
        UpdateCalibrationText();
    }

    public void UpdateCalibrationText()
    {
        calibrationText.text = (int)calibration.value + " ms";
    }

    public void OnApplyCalibration()
    {
        lastAppliedCalibration = calibration.value;
        Settings.istance.SetNotesLatencyOffset(calibration.value/1000);
        notesUIScript.Restart();
    }

    public void OnResetCalibration()
    {
        SetCalibration(lastAppliedCalibration);
    }

    public void OnSelectCalibration()
    {
        mainSettings.SetActive(false);
        calibrationSettings.SetActive(true);
        menuMusic.Pause();
        notesUIScript.Restart();
    }

    public void OnQuitCalibration()
    {
        notesUIScript.StopNotes();
        menuMusic.Play();
        calibrationSettings.SetActive(false);
        mainSettings.SetActive(true);
    }
}
