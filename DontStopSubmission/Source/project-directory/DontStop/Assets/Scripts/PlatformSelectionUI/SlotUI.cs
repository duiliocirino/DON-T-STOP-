using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image unselectedUI;
    public Image selectedUI;
    public Image lockImage;

    public bool isLocked { get; private set; } = false;

    private GameObject platform;

    private Image platformImage;

    private void Awake()
    {
        platformImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetPlatform(GameObject p)
    {
        platform = p;
        platformImage.sprite = PlatformSelectionUI.instance.platformScripts[platform].imagePreview;
    }

    public GameObject GetPlatform()
    {
        return platform;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setActive()
    {
        selectedUI.enabled = true;
        unselectedUI.enabled = false;
    }

    public void setInactive()
    {
        unselectedUI.enabled = true;
        selectedUI.enabled = false;
    }

    public void Lock()
    {
        isLocked = true;
        platformImage.enabled = false;
        lockImage.enabled = true;
    }

    public void Unlock()
    {
        isLocked = false;
        lockImage.enabled = false;
        platformImage.enabled = true;
    }
}
