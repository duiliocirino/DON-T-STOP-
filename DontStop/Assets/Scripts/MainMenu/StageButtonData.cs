using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtonData : MonoBehaviour
{
    public string scene;
    public bool clickable;

    //private GameObject lockBackground;
    private Button button;
    private Image buttonImage;
    private Color activeColor = new Color(1, 1, 1);
    private Color inactiveColor = new Color(0.66f, 0.66f, 0.66f);

    // Start is called before the first frame update
    void Start()
    {
        //lockBackground = transform.Find("LockBackground").gameObject;
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        //lockBackground.SetActive(!clickable);
        setInterractable(clickable);
    }

    // Update is called once per frame
    void Update()
    {
        if(clickable != button.interactable)
        {
            //lockBackground.SetActive(!clickable);
            setInterractable(clickable);
        }
    }

    private void setInterractable(bool b)
    {
        button.interactable = b;
        if (clickable)
        {
            buttonImage.color = activeColor;
        }
        else
        {
            buttonImage.color = inactiveColor;
        }
    }
}
