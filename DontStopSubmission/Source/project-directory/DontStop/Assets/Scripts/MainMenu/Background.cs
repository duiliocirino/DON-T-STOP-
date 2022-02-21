using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float referenceHeight = 1080;
    public float referenceWidth = 1920;
    // Start is called before the first frame update
    void Start()
    {
        float normalizedHeight = Screen.height / referenceHeight;
        float normalizedWidth = Screen.width / referenceWidth;
        if(normalizedHeight > normalizedWidth)
        {
            transform.localScale *= normalizedHeight/ normalizedWidth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
