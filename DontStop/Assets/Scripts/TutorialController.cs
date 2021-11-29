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
    private RectTransform weakPlatformDialogBoxRectTransform;

    private void Awake()
    {
        istance = this;
        rectTransform = GetComponent<RectTransform>();

        foreach (Transform child in transform)
            dialogBoxes.Add(child.name, child.gameObject);

        weakPlatformDialogBoxRectTransform = dialogBoxes["WeakPlatformDialogBox"].GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableDialogBox(string boxName, Vector3 position = new Vector3())
    {
        if (boxName.Equals("WeakPlatformDialogBox"))
        {
            Vector2 platformCanvasPosition = builderCamera.WorldToViewportPoint(position);
            platformCanvasPosition = new Vector2(
                (platformCanvasPosition.x * rectTransform.sizeDelta.x) - (rectTransform.sizeDelta.x * 0.5f),
                (platformCanvasPosition.y * rectTransform.sizeDelta.y) - (rectTransform.sizeDelta.y * 0.5f));
            weakPlatformDialogBoxRectTransform.position = platformCanvasPosition;
        }

        dialogBoxes[boxName].SetActive(true);
    }

    public void disableDialogBox(string boxName)
    {
        dialogBoxes[boxName].SetActive(false);
    }

    public IEnumerator showDialogBox(string boxName, float time, Vector3 position = new Vector3())
    {
        enableDialogBox(boxName, position);
        yield return new WaitForSeconds(time);
        disableDialogBox(boxName);
    }
}
