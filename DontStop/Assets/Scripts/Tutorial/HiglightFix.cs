using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiglightFix : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.gameObject.GetComponent<RectTransform>();
    }

    void Start()
    {
        float actualHeight = 1920 * Screen.height/Screen.width;

        float multiplier = (2*rectTransform.anchorMin.y)-1;

        rectTransform.anchoredPosition += multiplier * new Vector2(0, (actualHeight/2) - (1080/2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
