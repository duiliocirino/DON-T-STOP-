using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController istance { get; private set; }

    public Camera builderCamera;

    private Dictionary<string, GameObject> dialogBoxes = new Dictionary<string, GameObject>();
    private RectTransform rectTransform;

    private void Awake()
    {
        istance = this;
        rectTransform = GetComponent<RectTransform>();

        foreach (Transform child in transform)
            dialogBoxes.Add(child.name, child.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableDialogBox(string boxName)
    {
        dialogBoxes[boxName].SetActive(true);
    }

    public void disableDialogBox(string boxName)
    {
        dialogBoxes[boxName].SetActive(false);
    }

    public void disableAllDialogBoxes()
    {
        foreach(GameObject box in dialogBoxes.Values)
        {
            box.SetActive(false);
        }
    }

    public void showDialogBox(string boxName, float time)
    {
        StartCoroutine(_showDialogBox(boxName, time));
    }

    private IEnumerator _showDialogBox(string boxName, float time)
    {
        enableDialogBox(boxName);
        yield return new WaitForSeconds(time);
        disableDialogBox(boxName);
    }
}
