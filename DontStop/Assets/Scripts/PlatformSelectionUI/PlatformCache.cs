using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCache : MonoBehaviour
{
    public static PlatformCache instance { get; private set; }

    public List<GameObject> platformPrefabs;
    public Dictionary<GameObject, PlaneLogic> platformScripts = new Dictionary<GameObject, PlaneLogic>();

    public void Awake()
    {
        instance = this;
        foreach(GameObject p in platformPrefabs)
        {
            platformScripts.Add(p, p.GetComponent<PlaneLogic>());
        }
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
