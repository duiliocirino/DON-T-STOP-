using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlaneHandler : MonoBehaviour
{
    public static PlaneHandler instance;
    public List<GameObject> PlatformTiles => platformTiles;
    public List<GameObject> EmptyTiles => emptyTiles;
    public List<GameObject> PlatformPrefabs => platformPrefabs;
    public List<GameObject> ObstaclePrefabs => obstaclePrefabs;
    public ParticleSystem particles;
    public ParticleSystem wrongRhythmParticles;
    public ParticleSystem hitZoneCreatorParticle;
        
    public CameraShake shaker;

    public float probabilityBadPlat = 0.20f;
    public float spacing = 15f;
    public int laneNumbersRadius = 3; //laneNumbers = 2*laneNumberradius + 1
    public int numberOfEmptyTilesSide = 1;

    public bool tutorialPresent;
    public int platformInTutorial;
    public float platformSkippedAtTutorialEnd;

    public float stagePlaneLifeModifier = 1;

    public float TOLERANCE = 10;
    
    [SerializeField] internal List<GameObject> platformTiles = new List<GameObject>();
    [SerializeField] private List<GameObject> emptyTiles = new List<GameObject>();
    [SerializeField] private List<GameObject> platformPrefabs;
    [SerializeField] private List<GameObject> brokenPlatformPrefabs;

    [SerializeField] SoundChooser sound_goodPlatform;
    [SerializeField] SoundChooser sound_badPlatform;


    [SerializeField] private List<GameObject> obstaclePrefabs;
    
    private int diffTiles = 2;
    public float diffEmpProbability = 0.2f;
    [SerializeField] private GameObject emptyPrefab;
    [SerializeField] private List<GameObject> diffEmptyTiles;
    

    // Difficulty variables and references
    public int difficultyStep = 3;
    public float maximumDepletionRate = 0.12f;
    public float difficultyMultiplier = 1.2f;
    
    public NotesHandler notesHandler;
    public LifeBar lifeBar;

    private int startingNumPlatforms;

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
    
    private void GenerateBadPlatform(Vector3 position)
    {
        if (Random.value < probabilityBadPlat)
        {
            int lowRange = -numberOfEmptyTilesSide;
            while (position.x + (lowRange * spacing) < -laneNumbersRadius * spacing) lowRange++;
            int highRange = numberOfEmptyTilesSide;
            while (position.x + (highRange * spacing) > laneNumbersRadius * spacing) highRange--;

            int place = Random.Range(lowRange, highRange);

            Vector3 badPlatformPosition = new Vector3(position.x + (place * spacing), 0.0f, position.z + 2 * spacing);
            if (/*(platformInTutorial * spacing) < badPlatformPosition.z &&*/ badPlatformPosition.z < ((platformInTutorial + platformSkippedAtTutorialEnd + 3) * spacing))
                return;

            GameObject newPlatform = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count - 1)],
                badPlatformPosition, Quaternion.identity);
            platformTiles.Add(newPlatform);
        }
    }

    /**
     * This method adds a platform on Creator command.
     */
    public void AddPlatform(Vector3 position, GameObject prefab, GameObject emptyTile)
    {
        GameObject newPlatform;
        if (PlatformSelectionUI.instance.lastPreview != null)
        {
            PlatformSelectionUI.instance.DismantlePlatformPreview();
        }
        
        if (RhythmControllerUI.instance.noteInHitArea || TutorialController.instance.hitAlwaysTrue)
        {
            particles.transform.position = position;
            hitZoneCreatorParticle.Stop();
            hitZoneCreatorParticle.Play();
            particles.Play();
            sound_goodPlatform.PlayRand();
            newPlatform = Instantiate(prefab, position, Quaternion.identity);
        }
        else
        {
            particles.transform.position = position;
            
            wrongRhythmParticles.Play();
            sound_badPlatform.PlayRand();
            
            shaker.Enable();
            newPlatform = Instantiate(brokenPlatformPrefabs[platformPrefabs.IndexOf(prefab)], position, Quaternion.identity);
        }

        newPlatform.GetComponent<PlaneLogic>().planeLife *= stagePlaneLifeModifier;

        platformTiles.Add(newPlatform);
        RemoveSameLayerEmptyTiles(position);
        GenerateBadPlatform(position);
        AddEmptyTiles(position);
        IncreaseDifficulty();
        if(GameObject.FindWithTag("ControllerRTE") == null) return;
        TriggerRTE(newPlatform, emptyTile);
    }

    private void TriggerRTE(GameObject newPlatform, GameObject emptyTile)
    {
        PlatformLocking();
        
        if(emptyTile == null) return;
        
        if(PlatformTiles.Count > 2 && emptyTile.name.Contains("Lightning"))
            StartCoroutine(GeneralRTE.instance.LightningEvent(newPlatform));
    }

    private void PlatformLocking()
    {
        if (GameObject.FindWithTag("ControllerRTE") == null) return;
        CreatorRTE.instance.Unlocker();
        CreatorRTE.instance.Locker();
    }

    /**
     * This method adds a new layer of empty tiles after the creation of a new platform.
     */
    public void AddEmptyTiles(Vector3 position)
    {
        float zspacing = spacing;
        if (tutorialPresent && Math.Abs(position.z - platformInTutorial*spacing) < TOLERANCE)
            zspacing += platformSkippedAtTutorialEnd*spacing - 15;
        
        for (int i = -numberOfEmptyTilesSide; i <= numberOfEmptyTilesSide; i++)
        {
            Vector3 newEmptyPlatformPosition = new Vector3(position.x + (i * spacing), 0.0f, position.z + zspacing);
            if (-laneNumbersRadius * spacing <= newEmptyPlatformPosition.x && newEmptyPlatformPosition.x <= laneNumbersRadius * spacing)
            {
                if (!IsPlatformPresent(newEmptyPlatformPosition.x, newEmptyPlatformPosition.z, true))
                {
                    GameObject newEmp;
                    if (Random.Range(0.0f, 1.0f) < diffEmpProbability && diffEmptyTiles.Count > 0 && diffTiles > 0)
                    {
                        newEmp = diffEmptyTiles[Random.Range(0, diffEmptyTiles.Count)];
                        diffTiles--;
                    }
                    else
                    {
                        newEmp = emptyPrefab;
                    }
                    GameObject empty = Instantiate(newEmp, newEmptyPlatformPosition, Quaternion.identity);
                    emptyTiles.Add(empty);
                }
            }
        }

        diffTiles = 2;
        EnableEmptyTiles(PlatformSelectionUI.instance.GetSelectedPlatformSize());
    }

    public void RemovePlatform(GameObject obj)
    {
        platformTiles.Remove(obj);
        Destroy(obj);
    }

    internal void DisablePlatform(GameObject gameObject)
    {
        GameObject platform = platformTiles.Find(it => it == gameObject);
        if (platform != null)
        {
            platform.SetActive(false);
        }
    }

    public void RemoveOldPlanes(float z)
    {
        List<GameObject> toRemove = platformTiles.Where(platform => platform.transform.position.z < z).ToList();
        print(toRemove.Count());
        for (int i = toRemove.Count()-1; i>=0; i--)
        {
            GameObject platform = toRemove[i];
            toRemove.RemoveAt(i);
            RemovePlatform(platform);
        }
    }

    /**
     * This method removes an object from the emptyTiles list and then proceeds to destroy it.
     */
    public void RemoveEmptyTiles(GameObject obj)
    {
        emptyTiles.Remove(obj);
        Destroy(obj);
    }

    public void PopPlatform()
    {
        var platformTile = PlatformTiles[PlatformTiles.Count - 1];
        var position = platformTile.transform.position;
        RemoveSameLayerEmptyTiles(position + Vector3.forward * spacing);
        RemovePlatform(platformTile);
        AddEmptyTiles(PlatformTiles[PlatformTiles.Count - 1].transform.position);
    }

    /**
     * This method removes all the empty tiles from the z-layer where the Creator put a new platform.
     */
    void RemoveSameLayerEmptyTiles(Vector3 position)
    {
        List<GameObject> temp = new List<GameObject>();
        
        foreach (var tile in emptyTiles.Where(
            tile => Math.Abs(tile.transform.position.z - position.z) < TOLERANCE))
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
                    Math.Abs(tile.transform.position.x - x) < TOLERANCE &&
                    Math.Abs(tile.transform.position.z - z) < TOLERANCE))
                .Any() ||
            ((EmptyTiles.Where(tile =>
                    Math.Abs(tile.transform.position.x - x) < TOLERANCE &&
                    Math.Abs(tile.transform.position.z - z) < TOLERANCE))
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

    void IncreaseDifficulty()
    {
        //TODO: add graphical feedback to the audience
        if (notesHandler.notesCollected >= notesHandler.notesForNextStage && !SelectedStage.istance.story)
        {
            if (startingNumPlatforms == 0)
                startingNumPlatforms = platformTiles.Count;
            else if ((platformTiles.Count - startingNumPlatforms) % difficultyStep == 0)
            {
                float temp = lifeBar.depletionRate * difficultyMultiplier;
                if (temp < maximumDepletionRate)
                {
                    lifeBar.depletionRate *= difficultyMultiplier;
                    lifeBar.CalculateDepletionSpeed();
                }
                else
                {   
                    lifeBar.depletionRate = maximumDepletionRate;
                    lifeBar.CalculateDepletionSpeed();
                }
            }
        }
    }
    
    public GameObject GetPrefab(string name)
    {
        if (name.Length > 5)
        {
            if (name.Substring(name.Length - 6) == "broken")
            {
                return brokenPlatformPrefabs.First(platform => platform.name == name);
            }
            //Debug.Log(name);
            return platformPrefabs.First(platform => platform.name == name);
        }
        return null;
    }

    public GameObject GetNearestEmptyTile(Vector3 hitInfoPoint)
    {
        if (EmptyTiles.Count != 0)
        {
            var emptyTile = (EmptyTiles.Where(tile =>
                    Math.Abs(tile.transform.position.x - hitInfoPoint.x) < TOLERANCE &&
                    Math.Abs(tile.transform.position.z - hitInfoPoint.z) < TOLERANCE))
                .FirstOrDefault();
            if (emptyTile != null)
                return emptyTile;
        }
        return null;
    }
}