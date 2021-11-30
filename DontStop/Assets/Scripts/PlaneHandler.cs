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
    public List<GameObject> PlatformPrefabs => platformPrefabs;
    public List<GameObject> ObstaclePrefabs => obstaclePrefabs;

    public float probabilityBadPlat = 0.20f;
    public float spacing = 15f;
    public int laneNumbersRadius = 3; //laneNumbers = 2*laneNumberradius + 1
    public int numberOfEmptyTilesSide = 1;

    public bool tutorialPresent;

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
    
    private void GenerateBadPlatform(Vector3 position)
    {
        if (Random.value < probabilityBadPlat)
        {
            int place = Random.Range(-numberOfEmptyTilesSide, numberOfEmptyTilesSide);
            GameObject newPlatform = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count - 1)],
                new Vector3(position.x + (place * spacing), 0.0f, position.z + 2 * spacing), Quaternion.identity);
            platformTiles.Add(newPlatform);
        }
    }

    /**
     * This method adds a platform on Creator command.
     */
    public void AddPlatform(Vector3 position, GameObject prefab)
    {
        GameObject newPlatform = Instantiate(prefab, position, Quaternion.identity);
        platformTiles.Add(newPlatform);
        RemoveSameLayerEmptyTiles(position);
        GenerateBadPlatform(position);
    }

    /**
     * This method adds a new layer of empty tiles after the creation of a new platform.
     */
    public void AddEmptyTiles(Vector3 position)
    {
        float zspacing = spacing;
        if (tutorialPresent && position.z == 135)
            zspacing += 30;

        for (int i = -numberOfEmptyTilesSide; i <= numberOfEmptyTilesSide; i++)
        {
            Vector3 newEmptyPlatformPosition = new Vector3(position.x + (i * spacing), 0.0f, position.z + zspacing);
            if (-laneNumbersRadius * spacing <= newEmptyPlatformPosition.x && newEmptyPlatformPosition.x <= laneNumbersRadius * spacing)
            {
                if (!IsPlatformPresent(newEmptyPlatformPosition.x, newEmptyPlatformPosition.z, true))
                {
                    GameObject empty = Instantiate(emptyPrefab, newEmptyPlatformPosition, Quaternion.identity);
                    emptyTiles.Add(empty);
                }
            }
        }

        EnableEmptyTiles(PlatformSelectionUI.instance.GetSelectedPlatformSize());
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

    public bool IsPlatformPresent(float x, float z, bool checkEmpty)
    {
        if ((PlatformTiles.Where(tile =>
                    tile.transform.position.x == x &&
                    tile.transform.position.z == z))
                .Any() ||
            ((EmptyTiles.Where(tile =>
                    tile.transform.position.x == x &&
                    tile.transform.position.z == z))
                .Any() && checkEmpty))
            return true;
        return false;
    }

    public void ComunicateSelectedPlatformSize(int size)
    {
        EnableEmptyTiles(size);
    }

    public void EnableEmptyTiles(int size)
    {
        foreach (GameObject e in emptyTiles)
        {
            int platformRadius = size == 0 ? 0 : (size - 1) / 2;
            int actualLaneNumberRadius = laneNumbersRadius - platformRadius;
            bool selectedPlaneWontExitBudries = -actualLaneNumberRadius * spacing <= e.transform.position.x && e.transform.position.x <= actualLaneNumberRadius * spacing;
            bool selectedPlaneWontOverlapObstacle = !IsPlatformPresentRange(e.transform.position.x, e.transform.position.z, platformRadius);
            e.SetActive(selectedPlaneWontExitBudries && selectedPlaneWontOverlapObstacle);
        }
    }

    public bool IsPlatformPresentRange(float centerX, float centerZ, int radius)
    {
        for (int i = -radius; i<= radius; i++)
        {
            if (IsPlatformPresent(centerX + i * spacing, centerZ, false))
            {
                return true;
            }
        }

        return false;
    }
}