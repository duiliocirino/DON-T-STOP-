using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSelectionUI : MonoBehaviour
{
    public List<GameObject> slots;
    private RandomRotatingPool<GameObject> pool;
    private List<SlotUI> slotScripts = new List<SlotUI>();

    private int selectedSlotIndex = -1;
    

    private void Awake()
    {
        pool = new RandomRotatingPool<GameObject>(PlatformCache.instace.platformPrefabs);

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
                print("start: " + selectedSlotIndex);
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
                print("stop: " + old);
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
        slotScripts[selectedSlotIndex].SetPlatform(pool.GetNext());
        return platform;
    }
}
