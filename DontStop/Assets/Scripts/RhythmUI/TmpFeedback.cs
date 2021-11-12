using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TmpFeedback : MonoBehaviour
{
    private Image image;
    private Color correctColor = new Color(0, 1, 0);
    private Color incorrectColor = new Color(1, 0, 0);

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
        //TODO new input system
        /*if (Input.GetButtonDown("Jump"))
        {
            if (RhythmControllerUI.instance.noteInHitArea)
            {
                image.color = correctColor;
            }
            else
            {
                image.color = incorrectColor;
            }
        }*/
    }
}
