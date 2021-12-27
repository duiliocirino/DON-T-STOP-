using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController instance { get; private set; }

    public Camera builderCamera;
    public bool hitAlwaysTrue = false;

    public List<GameObject> dialogBoxes = new List<GameObject>();
    private RectTransform rectTransform;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableDialogBox(int i)
    {
        dialogBoxes[i].SetActive(true);
    }

    public void disableDialogBox(int i)
    {
        dialogBoxes[i].SetActive(false);
    }

    public void disableAllDialogBoxes()
    {
        foreach (GameObject box in dialogBoxes)
        {
            box.SetActive(false);
        }
    }

    public void showDialogBox(int i, float time)
    {
        StartCoroutine(_showDialogBox(i, time));
    }

    private IEnumerator _showDialogBox(int i, float time)
    {
        enableDialogBox(i);
        yield return new WaitForSeconds(time);
        disableDialogBox(i);
    }
}
