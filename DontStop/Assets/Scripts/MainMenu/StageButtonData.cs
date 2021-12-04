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

    // Start is called before the first frame update
    void Start()
    {
        //lockBackground = transform.Find("LockBackground").gameObject;
        button = GetComponent<Button>();

        //lockBackground.SetActive(!clickable);
        button.enabled = clickable;
    }

    // Update is called once per frame
    void Update()
    {
        if(clickable != button.enabled)
        {
            //lockBackground.SetActive(!clickable);
            button.enabled = clickable;
        }
    }
}
