using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Creator : MonoBehaviour
{
    // private int rayDistance = 1000;
    public Camera creatorCamera;
    //private CreatorRTE _creatorRte;
    private float timeFromLastCreation;

    private float spacing = 15f;
    
    // Start is called before the first frame update
    void Awake()
    {
        // controls = new PlayerControls();
        //_creatorRte = gameObject.AddComponent<CreatorRTE>();
    }

    private bool _firstPlace = true;
    private double TOLERANCE = 5;
    public int maxDistance = 35;

    private void OnClickCreate()
    {
        spacing = PlaneHandler.instance.spacing;
        Vector3 spawnPosition;
        var ray = creatorCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        int layerMask = 1 << 8;
        //Debug.Log("OnClick called");
        if (PlatformSelectionUI.instance.isSlotSelected() && !Pause.paused && timeFromLastCreation > 0.25f)
        {
            if (_firstPlace)
            {
                //TutorialController.instance.disableDialogBox("TutorialCreator");
                _firstPlace = false;
            }
            //Debug.Log("Left click on " + hit.collider.gameObject.name);
            //GameObject emptyTile = PlaneHandler.instance.GetNearestEmptyTile(hit.point);
            GameObject emptyTile = PlatformSelectionUI.instance.lastPreview;
            if (emptyTile != null)
                spawnPosition = emptyTile.transform.position;
            else 
                spawnPosition = Vector3.zero;
            if (!(PlaneHandler.instance.IsPlatformPresent(spawnPosition.x, spawnPosition.z, false)) && emptyTile != null)
            {
                GameObject newPlatform;
                if (!(PlaneHandler.instance.tutorialPresent && Math.Abs(spawnPosition.z - (PlaneHandler.instance.platformInTutorial-1)* PlaneHandler.instance.spacing) < TOLERANCE))
                    newPlatform = PlatformSelectionUI.instance.PlaceAndChangeSelectedPlatform();
                else
                    newPlatform = PlatformSelectionUI.instance.PlaceSelectedPlatformAndPutTrampolineNext();

                if (newPlatform != null)
                {
                    PlaneHandler.instance.AddPlatform(new Vector3(spawnPosition.x, 0, spawnPosition.z),
                        newPlatform, PlaneHandler.instance.GetNearestEmptyTile(emptyTile.transform.position));
                    if (!RhythmControllerUI.instance.noteInHitArea) {
                        LifeBar.instance.WorstMiss();
                    }
                    else {
                        LifeBar.instance.PerfectHit();
                    }

                    timeFromLastCreation = 0;
                }
            }
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        timeFromLastCreation += Time.deltaTime;
    }
}