using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image unselectedUI;
    public Image selectedUI;

    private GameObject platform;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetPlatform(GameObject p)
    {
        platform = p;
        //image.color = PlatformCache.instance.platformScripts[platform].preview;
        image.sprite = PlatformCache.instance.platformScripts[platform].imagePreview;
        //print(image.color);
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
}
