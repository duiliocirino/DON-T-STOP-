using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursor : MonoBehaviour
{
    public Camera sceneCamera;

    private RectTransform rectTransform;
    private RectTransform parentRectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.gameObject.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        rectTransform.anchoredPosition = new Vector2(mousePos.x * parentRectTransform.sizeDelta.x / Screen.width, mousePos.y * parentRectTransform.sizeDelta.y / Screen.height);
    }
}
