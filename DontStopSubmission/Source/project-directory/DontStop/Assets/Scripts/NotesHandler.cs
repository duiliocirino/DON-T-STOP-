using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NotesHandler : MonoBehaviour
{
    public GameObject notePrefab;
    
    public int notesForNextStage = 20;
    public int notesCollected = 0;
    public float minSpawnRangeZ = 3;
    public float maxSpawnRangeZ = 5;
    public float spawnProbability = 1f;
    public float baseSpawnProbability;
    public float timeSinceLastNote;

    [SerializeField] AudioSource noteTakenSound;

    public List<Action> onEnoughNotesCollected = new List<Action>();

    public Text UI;

    public float slope = 10000;
    // Start is called before the first frame update
    void Start()
    {
        string text = "0";
        if (notesForNextStage != 0)
            text += "/" + notesForNextStage;

        UI.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnNote();
        IncreaseSpawnProbability();
    }

    private void CheckNextStage()
    {
        
    }

    public void NoteTaken()
    {
        noteTakenSound.Play();
        notesCollected++;

        string text = notesCollected.ToString();
        if (notesForNextStage!=0)
            text += "/" + notesForNextStage;

        UI.text = text;

        if (notesCollected == notesForNextStage) OnEnoughNotesCollected();
    }

    private void OnEnoughNotesCollected()
    {
        //TutorialController.instance.showDialogBox("StageUnlocked", 3f);
        foreach (Action a in onEnoughNotesCollected)
            a.Invoke();
    }

    void ResetProbability()
    {
        spawnProbability = baseSpawnProbability;
        timeSinceLastNote = 0f;
    }
    
    void IncreaseSpawnProbability()
    {
        timeSinceLastNote += Time.deltaTime;
        spawnProbability = 1 - Mathf.Exp(-timeSinceLastNote / slope);
    }

    /**
     * This method places a new note with probability spawnProbability, on PlatformCreation.
     */
    void SpawnNote()
    {
        if (Random.value < spawnProbability)
        {
            Vector3 lastPlatformPosition = PlaneHandler.instance.PlatformTiles.Last().transform.position;
            float rangeX = PlaneHandler.instance.laneNumbersRadius * PlaneHandler.instance.spacing;
            float displacementZ = Random.Range(minSpawnRangeZ, maxSpawnRangeZ);
            float leftX = Math.Max(-rangeX, lastPlatformPosition.x - (displacementZ * PlaneHandler.instance.spacing));
            float rightX = Math.Min(rangeX, lastPlatformPosition.x + displacementZ * PlaneHandler.instance.spacing);
            Vector3 newNotePosition = new Vector3(Random.Range(leftX, rightX), Random.value, lastPlatformPosition.z + displacementZ * PlaneHandler.instance.spacing);
            if ((PlaneHandler.instance.platformInTutorial * PlaneHandler.instance.spacing) >= newNotePosition.z || newNotePosition.z >= ((PlaneHandler.instance.platformInTutorial + PlaneHandler.instance.platformSkippedAtTutorialEnd + 1) * PlaneHandler.instance.spacing))
                GameObject.Instantiate(notePrefab, newNotePosition, Quaternion.identity);
            ResetProbability();
        }
    }
}