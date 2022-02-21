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
        image.sprite = PlatformSelectionUI.instance.platformScripts[platform].imagePreview;
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
