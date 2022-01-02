using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformSelectionUI : MonoBehaviour
{
    public static PlatformSelectionUI instance { get; private set; }

    public List<GameObject> platformPrefabs;
    public Dictionary<GameObject, PlaneLogic> platformScripts = new Dictionary<GameObject, PlaneLogic>();

    //TODO: adjust and pick the platforms from the PlaneHandler to have coherency
    public List<GameObject> slots;
    private RandomRotatingPool<GameObject> pool;
    private List<SlotUI> slotScripts = new List<SlotUI>();

    private int selectedSlotIndex = -1;
    public Camera camera;
    public Material previewMaterial;
    private Dictionary<string, Material> formerMaterial = new Dictionary<string, Material>();
    public GameObject lastPreview;
    private GameObject lastEmptyTile;

    private void Awake()
    {
        instance = this;

        foreach (GameObject p in platformPrefabs)
        {
            platformScripts.Add(p, p.GetComponent<PlaneLogic>());
        }
        
        pool = new RandomRotatingPool<GameObject>(platformPrefabs);
    }

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        //TODO switch input system
        if(selectedSlotIndex < 0 || selectedSlotIndex >= slots.Count)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) selectedSlotIndex = 0;
            if (Input.GetKeyDown(KeyCode.DownArrow)) selectedSlotIndex = 1;
            if (Input.GetKeyDown(KeyCode.RightArrow)) selectedSlotIndex = 2;

            if(selectedSlotIndex >= 0 && selectedSlotIndex < slots.Count)
            {
                slotScripts[selectedSlotIndex].setActive();
                PlaneHandler.instance.ComunicateSelectedPlatformSize(platformScripts[slotScripts[selectedSlotIndex].GetPlatform()].platformSize);
                //print("start: " + selectedSlotIndex);
                CreatePlatformPreview();
            }
        }
        else
        {
            int old = selectedSlotIndex;
            switch (selectedSlotIndex)
            {
                case 0:
                    if (Input.GetKeyUp(KeyCode.LeftArrow)) selectedSlotIndex = -1;
                    break;
                case 1:
                    if (Input.GetKeyUp(KeyCode.DownArrow)) selectedSlotIndex = -1;
                    break;
                case 2:
                    if (Input.GetKeyUp(KeyCode.RightArrow)) selectedSlotIndex = -1;
                    break;
                default: break;
            }

            if (selectedSlotIndex == -1)
            {
                slotScripts[old].setInactive();
                PlaneHandler.instance.ComunicateSelectedPlatformSize(0);
                //print("stop: " + old);
                DismantlePlatformPreview();
            }
            
            var ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            int layerMask = 1 << 8;
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) ||
                 Input.GetKey(KeyCode.RightArrow)) &&
                Physics.Raycast(ray, out var hit, 45, layerMask) && !Pause.paused)
                if (lastEmptyTile != PlaneHandler.instance.GetNearestEmptyTile(hit.point) && PlaneHandler.instance.GetNearestEmptyTile(hit.point) != null)
                    CreatePlatformPreview();
        }
    }

    public void SetSelectedSlotIndex(int index)
    {
        selectedSlotIndex = index;
    }

    public void CreatePlatformPreview()
    {
        if(lastPreview != null)
            DismantlePlatformPreview();
        var ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        int layerMask = 1 << 8;
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) ||
             Input.GetKey(KeyCode.RightArrow)) &&
            Physics.Raycast(ray, out var hit, 45, layerMask) && !Pause.paused)
        {
            lastEmptyTile = PlaneHandler.instance.GetNearestEmptyTile(hit.point);
            Vector3 tilePos = lastEmptyTile.transform.position;
            if(lastEmptyTile != null)
            {
                lastEmptyTile.GetComponent<MeshRenderer>().enabled = false;
            }
            GameObject platformPrefab = slotScripts[selectedSlotIndex].GetPlatform();
            lastPreview = Instantiate(platformPrefab, tilePos, Quaternion.identity);
            var colliders = lastPreview.GetComponentsInChildren<BoxCollider>();
            foreach (var collider in colliders)
            {
                Destroy(collider);
            }
            var renderers = lastPreview.GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRenderer in renderers)
            {
                formerMaterial.Add(meshRenderer.gameObject.name, meshRenderer.material);
                meshRenderer.material = previewMaterial;
            }
        }
    }

    public void DismantlePlatformPreview()
    {
        if (lastPreview != null)
        {
            var renderers = lastPreview.GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRenderer in renderers)
            {
                meshRenderer.material = formerMaterial[meshRenderer.gameObject.name];
            }
            if(lastEmptyTile != null)
            {
                lastEmptyTile.GetComponent<MeshRenderer>().enabled = true;
            }

            Destroy(lastPreview);
            formerMaterial = new Dictionary<string, Material>();
            lastPreview = null;
        }
    }

    public GameObject PlaceAndChangeSelectedPlatform()
    {
        if(selectedSlotIndex<0 || selectedSlotIndex>=slots.Count)
        {
            return null;
        }

        GameObject platform = slotScripts[selectedSlotIndex].GetPlatform();

        if (savedPlatforms == null)
        {
            GameObject nextPlatform = pool.GetNext();
            slotScripts[selectedSlotIndex].SetPlatform(nextPlatform);
        }
        else
        {
            RestorePlatforms();
        }
        return platform;
    }

    public int GetSelectedPlatformSize()
    {
        return selectedSlotIndex == -1 ? 0 : platformScripts[slotScripts[selectedSlotIndex].GetPlatform()].platformSize;
    }

    private GameObject[] savedPlatforms = null;
    public GameObject PlaceSelectedPlatformAndPutTrampolineNext()
    {
        GameObject platform = PlaceAndChangeSelectedPlatform();
        GameObject trampoline = platformPrefabs[5];

        savedPlatforms = new GameObject[slotScripts.Count];
        for(int i=0; i<slotScripts.Count; i++)
        {
            savedPlatforms[i] = slotScripts[i].GetPlatform();
            slotScripts[i].SetPlatform(trampoline);
        }

        return platform;
    }

    private void RestorePlatforms()
    {
        for (int i = 0; i < slotScripts.Count; i++)
        {
            slotScripts[i].SetPlatform(savedPlatforms[i]);
        }

        savedPlatforms = null;
    }

    public bool isSlotSelected()
    {
        return 0 <= selectedSlotIndex && selectedSlotIndex < slots.Count;
    }

}


