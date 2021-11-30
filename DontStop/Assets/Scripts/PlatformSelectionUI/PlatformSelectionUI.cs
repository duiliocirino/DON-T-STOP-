using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSelectionUI : MonoBehaviour
{
    public static PlatformSelectionUI instance { get; private set; }

    //TODO: adjust and pick the platforms from the PlaneHandler to have coherency
    public List<GameObject> slots;
    private RandomRotatingPool<GameObject> pool;
    private List<SlotUI> slotScripts = new List<SlotUI>();

    private int selectedSlotIndex = -1;
    

    private void Awake()
    {
        instance = this; 
        
        pool = new RandomRotatingPool<GameObject>(PlatformCache.instance.platformPrefabs);

        slotScripts = new List<SlotUI>();
        foreach (GameObject slot in slots)
        {
            SlotUI script = slot.GetComponent<SlotUI>();
            slotScripts.Add(script);
            GameObject p = pool.GetNext();
            script.SetPlatform(p);
            script.setInactive();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //TODO switch input system
        if(selectedSlotIndex < 0 || selectedSlotIndex >= slots.Count)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSlotIndex = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSlotIndex = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSlotIndex = 2;

            if(selectedSlotIndex >= 0 && selectedSlotIndex < slots.Count)
            {
                slotScripts[selectedSlotIndex].setActive();
                PlaneHandler.instance.ComunicateSelectedPlatformSize(PlatformCache.instance.platformScripts[slotScripts[selectedSlotIndex].GetPlatform()].platformSize);
                //print("start: " + selectedSlotIndex);
            }
        }
        else
        {
            int old = selectedSlotIndex;
            switch (selectedSlotIndex)
            {
                case 0:
                    if (Input.GetKeyUp(KeyCode.Alpha1)) selectedSlotIndex = -1;
                    break;
                case 1:
                    if (Input.GetKeyUp(KeyCode.Alpha2)) selectedSlotIndex = -1;
                    break;
                case 2:
                    if (Input.GetKeyUp(KeyCode.Alpha3)) selectedSlotIndex = -1;
                    break;
                default: break;
            }

            if (selectedSlotIndex == -1)
            {
                slotScripts[old].setInactive();
                PlaneHandler.instance.ComunicateSelectedPlatformSize(0);
                //print("stop: " + old);
            }
        }
    }

    public void SetSelectedSlotIndex(int index)
    {
        selectedSlotIndex = index;
    }

    public GameObject PlaceAndChangeSelectedPlatform()
    {
        if(selectedSlotIndex<0 || selectedSlotIndex>=slots.Count)
        {
            return null;
        }

        GameObject platform = slotScripts[selectedSlotIndex].GetPlatform();
        GameObject nextPlatform = pool.GetNext();

        slotScripts[selectedSlotIndex].SetPlatform(nextPlatform);
        return platform;
    }

    public int GetSelectedPlatformSize()
    {
        return selectedSlotIndex == -1 ? 0 : PlatformCache.instance.platformScripts[slotScripts[selectedSlotIndex].GetPlatform()].platformSize;
    }

    public GameObject PlaceSelectedPlatformAndPutTrampolineNext()
    {
        print("aaaaaaaaaaaaa");

        if (selectedSlotIndex < 0 || selectedSlotIndex >= slots.Count)
        {
            return null;
        }

        GameObject platform = slotScripts[selectedSlotIndex].GetPlatform();
        GameObject nextPlatform = PlatformCache.instance.platformPrefabs[5];

        slotScripts[selectedSlotIndex].SetPlatform(nextPlatform);
        return platform;
    }
}
