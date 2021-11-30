using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
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
        image.color = PlatformCache.instance.platformScripts[platform].preview;
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
    }

    public void setInactive()
    {
        selectedUI.enabled = false;
    }
}
