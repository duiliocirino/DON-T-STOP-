using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLogic : MonoBehaviour
{
    public int platformSize = 1;
    protected bool isPlayerOn = false;
    [SerializeField] protected float planeLife = 5f;
    [SerializeField] protected float missPenalty = 0.5f;
    protected float timeOn = 0f;
    public Color preview;
    public Sprite imagePreview;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOn)
        {
            timeOn += Time.deltaTime;
        }

        //if (timeOn < planeLife && timeOn >= planeLife * 0.75f) Debug.Log("Only" + (planeLife - timeOn) + "are left for the platform");
        if (timeOn >= planeLife)
        {
            PlaneHandler.instance.RemovePlatform(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        isPlayerOn = true;
    }

    private void OnCollisionExit(Collision other)
    {
        isPlayerOn = false;
    }
}
