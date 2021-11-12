using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TmpBuzzer : MonoBehaviour
{
    private Image image;

    private bool previoslyActive = false;
    private Color activeColor = new Color(1, 1, 0);
    private Color inactiveColor = new Color(0, 0, 0);

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!previoslyActive && RhythmControllerUI.instance.noteInHitArea)
        {
            image.color = activeColor;
            previoslyActive = true;
        }

        if (previoslyActive && !RhythmControllerUI.instance.noteInHitArea)
        {
            image.color = inactiveColor;
            previoslyActive = false;
        }
    }
}
