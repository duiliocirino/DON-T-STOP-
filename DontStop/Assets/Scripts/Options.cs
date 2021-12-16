using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public static Options istance { private set; get; }

    public bool tutorial;
    public bool gameEnds;

    public string oldScene = "MainMenu";
    public int graphicsQuality = 1;

    private void Awake()
    {
        istance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
