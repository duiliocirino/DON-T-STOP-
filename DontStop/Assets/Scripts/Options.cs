using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public static Options istance { private set; get; }

    public bool tutorial;
    public bool gameEnds;

    private void Awake()
    {
        istance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!tutorial)
        {
            TutorialController.instance.hitAlwaysTrue = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
