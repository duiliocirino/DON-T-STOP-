using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public static Settings istance { private set; get; } = null;

    public int graphicsQuality { private set; get; }

    public Resolution[] resolutions { private set; get; }
    public int resolutionIndex { private set; get; }

    public bool fullScreen { private set; get; }

    public float volume { private set; get; }

    public AudioMixer mixer;

    private void Awake()
    {
        if (istance == null)
        {
            istance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("graphicsQuality"))
        {
            SetGraphicsQuality(1);
        }
        else
        {
            ChangeGraphicsQuality(PlayerPrefs.GetInt("graphicsQuality"));
        }

        resolutions = Screen.resolutions;
        if (!PlayerPrefs.HasKey("resolutionWidth"))
        {
            Resolution current = Screen.currentResolution;
            resolutionIndex = GetResolutionIndex(current);
            if (resolutionIndex == -1)
            {
                SetResolution(0);
            }
            else
            {
                PlayerPrefs.SetInt("resolutionWidth", current.width);
                PlayerPrefs.SetInt("resolutionHeight", current.height);
            }
        }
        else
        {
            Resolution target = new Resolution();
            target.width = PlayerPrefs.GetInt("resolutionWidth");
            target.height = PlayerPrefs.GetInt("resolutionHeight");
            int index = GetResolutionIndex(target);
            if (index == -1) index = 0;
            SetResolution(index);
        }

        if (!PlayerPrefs.HasKey("fullScreen"))
        {
            SetFullScreen(true);
        }
        else
        {
            fullScreen = PlayerPrefs.GetInt("fullScreen") != 0;
            Screen.fullScreen = fullScreen;
        }

        if (!PlayerPrefs.HasKey("volume"))
        {
            SetVolume(0.75f);
        }
        else
        {
            volume = PlayerPrefs.GetFloat("volume");
            SetMixerVolume(volume);
        }

        PlayerPrefs.Save();
    }

    private int GetResolutionIndex(Resolution target)
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution r = resolutions[i];
            if (r.width == target.width && r.height == target.height)
            {
                return i;
            }
        }
        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGraphicsQuality(int value)
    {
        if (0 <= value && value <= 2)
        {
            PlayerPrefs.SetInt("graphicsQuality", value);
            ChangeGraphicsQuality(value);
            PlayerPrefs.Save();
        }
    }

    private void ChangeGraphicsQuality(int quality)
    {
        if (0 <= quality && quality <= 2)
        {
            graphicsQuality = quality;
            QualitySettings.SetQualityLevel(quality);
        }
    }

    public void SetResolution(int index)
    {
        resolutionIndex = index;
        Resolution r = resolutions[resolutionIndex];
        Screen.SetResolution(r.width, r.height, fullScreen);
        PlayerPrefs.SetInt("resolutionWidth", r.width);
        PlayerPrefs.SetInt("resolutionHeight", r.height);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        fullScreen = isFullScreen;
        PlayerPrefs.SetInt("fullScreen", isFullScreen ? 1:0);
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.Save();
    }

    public void SetVolume(float vol)
    {
        if (0f <= vol && vol <= 1f)
        {
            volume = vol;
            SetMixerVolume(vol);
            PlayerPrefs.SetFloat("volume", volume);
            PlayerPrefs.Save();
        }
    }

    private void SetMixerVolume(float vol)
    {
        if (vol < 0.0001f)
        {
            mixer.SetFloat("MasterVolume", -80f);
        }
        else
        {
            mixer.SetFloat("MasterVolume", Mathf.Log10(vol) * 20);
        }
    }
}
