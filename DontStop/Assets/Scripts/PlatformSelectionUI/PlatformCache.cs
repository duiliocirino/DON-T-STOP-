using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCache : MonoBehaviour
{
    public static PlatformCache instace;

    public List<GameObject> platformPrefabs;
    public Dictionary<GameObject, Color> platformImmages = new Dictionary<GameObject, Color>();

    public void Awake()
    {
        instace = this;
        foreach(GameObject p in platformPrefabs)
        {
            platformImmages.Add(p, p.GetComponent<Renderer>().sharedMaterial.color);
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
