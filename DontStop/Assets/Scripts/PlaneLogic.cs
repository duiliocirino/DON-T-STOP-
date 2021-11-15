using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLogic : MonoBehaviour
{
    bool isPlayerOn = false;
    [SerializeField] float planeLife = 5f;
    [SerializeField] private float missPenalty = 0.5f;
    float timeOn = 0f;
    private GameObject thisPlane;
    
    // Start is called before the first frame update
    void Start()
    {
        thisPlane = this.gameObject;
        if (!RhythmControllerUI.instance.noteInHitArea) planeLife = planeLife * missPenalty;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOn)
        {
            timeOn += Time.deltaTime;
        }

        if (timeOn < planeLife && timeOn >= planeLife * 0.75f) Debug.Log("Only" + (planeLife - timeOn) + "are left for the platform");
        else if (timeOn >= planeLife)
        {
            Debug.Log("Plane got destroyed");
            Destroy(thisPlane);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("PlayerOnPlane");
        isPlayerOn = true;
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("PlayerOffPlane");
        isPlayerOn = false;
    }
}
