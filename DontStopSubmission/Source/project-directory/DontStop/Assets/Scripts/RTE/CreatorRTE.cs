using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorRTE : MonoBehaviour
{
    public static CreatorRTE instance;
    private int badPlatforms;
    public List<int> locked_index; 
    public float probability = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Locker()
    {
        if (!RhythmControllerUI.instance.noteInHitArea)
            badPlatforms++;
        float sample = Random.Range(0f, 1f);
        if (badPlatforms > 2)
        {
            for (int i = 0; i < 2; i++)
            {
                while (locked_index.Count <= i)
                {
                    int temp = Random.Range(0, 3);
                    if (!locked_index.Contains(temp))
                    {
                        locked_index.Add(temp);
                        PlatformSelectionUI.instance.LockSlot(temp);
                    }
                }
            }
            badPlatforms = 0;
        }
    }

    public void Unlocker()
    {
        if (locked_index.Count == 0) return;
        foreach (int index in locked_index)
        {
            Debug.Log("Unlocking " + index);
            PlatformSelectionUI.instance.UnlockSlot(index);
        }
        locked_index.Clear();
    }
}
