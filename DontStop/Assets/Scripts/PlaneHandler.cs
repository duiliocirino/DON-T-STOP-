using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaneHandler : MonoBehaviour
{
    public static PlaneHandler instance;
    public List<GameObject> PlatformTiles => platformTiles;
    public List<GameObject> EmptyTiles => emptyTiles;

    [SerializeField] public float spacing = 15f;

    [SerializeField] private int numberOfEmptyTilesSide = 1;
        
    [SerializeField] private List<GameObject> platformTiles = new List<GameObject>();
    [SerializeField] private List<GameObject> emptyTiles = new List<GameObject>();
    [SerializeField] private List<GameObject> platformPrefabs;
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private GameObject emptyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: complete this and binomial
    private void GenerateBadPlatform(Vector3 position)
    {
        if (Random.Range(0.3f, 0.6f) >= 0.5)
        {
            
        }
    }

    /**
     * This method adds a platform on Creator command.
     */
    public void AddPlatform(Vector3 position, string objName)
    {
        GameObject newPlatform = Instantiate(platformPrefabs.First(platform => platform.name == objName), position, Quaternion.identity);
        platformTiles.Add(newPlatform);
        RemoveSameLayerEmptyTiles(position);
        GenerateBadPlatform(position);
    }

    /**
     * This method adds a new layer of empty tiles after the creation of a new platform.
     */
    public void AddEmptyTiles(Vector3 position)
    {
        for (int i = -numberOfEmptyTilesSide; i <= numberOfEmptyTilesSide; i++)
        {
            if (!IsPlatformPresent(position.x + (i * spacing), position.z + spacing, false))
            {
                GameObject empty = Instantiate(emptyPrefab, new Vector3(position.x + (i * spacing), 0.0f, position.z + spacing), Quaternion.identity);
                emptyTiles.Add(empty);                    
            }
        }
    }

    public void RemovePlatform(GameObject obj)
    {
        
    }

    /**
     * This method removes an object from the emptyTiles list and then proceeds to destroy it.
     */
    public void RemoveEmptyTiles(GameObject obj)
    {
        emptyTiles.Remove(obj);
        Destroy(obj);
    }
    
    /**
     * This method removes all the empty tiles from the z-layer where the Creator put a new platform.
     */
    void RemoveSameLayerEmptyTiles(Vector3 position)
    {
        List<GameObject> temp = new List<GameObject>();
        
        foreach (var tile in emptyTiles.Where(
            tile => tile.transform.position.z == position.z))
        {
            temp.Add(tile);
        }

        foreach (var tile in temp)
        {
            RemoveEmptyTiles(tile);
        }
    }

    public bool IsPlatformPresent(float x, float z, bool notCheckEmpty)
    {
        if ((PlatformTiles.Where(tile =>
                    tile.transform.position.x == x &&
                    tile.transform.position.z == z))
                .Any() &&
            ((EmptyTiles.Where(tile =>
                    tile.transform.position.x == x &&
                    tile.transform.position.z == z))
                .Any() || notCheckEmpty))
            return true;
        return false;
    }
}