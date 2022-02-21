using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtonData : MonoBehaviour
{
    public int stageNumber = 0;
    public int unlocksWithStage = 0;
    public string scene = "MainScene";
    public bool bSide = false;

    //private GameObject lockBackground;
    private Button button;

    private void Awake()
    {
        //lockBackground = transform.Find("LockBackground").gameObject;
        button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //lockBackground.SetActive(!clickable);
        setInterractable(SaveController.istance.IsStageUnlocked(unlocksWithStage));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void setInterractable(bool b)
    {
        button.interactable = b;
    }

    public void debug(string s)
    {
        Debug.Log(s);
    }
}
