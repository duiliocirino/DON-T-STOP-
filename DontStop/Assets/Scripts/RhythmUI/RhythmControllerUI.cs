using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RhythmControllerUI : MonoBehaviour
{
    public float hitTime; //in fraction of a note
    public GameObject notePrefab;
    public AudioSource musicPlayer;
    public TextAsset patternMapJSON;
    public BoxCollider2D hitZone;

    public static RhythmControllerUI instance { get; private set; }
    public bool hasStarted = false;
    public Vector2 noteBufferPosition;
    public bool noteInHitArea = false;
    public float noteDespawnDelay;

    public PatternMap patternMap;

    private float speed;
    private float BPM;
    private float firstNoteDistance;
    private List<float> distanceVector;
    private int distanceVectorIndex = 0;

    private RectTransform rectTransform;

    private List<GameObject> noteBufer;
    private List<NoteUI> noteScripts;
    private List<RectTransform> noteRectTransforms;
    private int nextNotes = 0;
    private int previousNotes = -1;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();

        //for now I generate the pattern map here;
        patternMap = GeneratePatternMap();

        speed = patternMap.noteSpeed;
        BPM = patternMap.BPM;
        GenerateDistanceVector();

        GenerateNotes();

        GenerateHitZones();
    }

    private void GenerateHitZones()
    {
        float noteLength = speed * (60/BPM);

        hitZone.size = new Vector2(noteLength * hitTime, 50);

        noteDespawnDelay = (60/BPM) * hitTime;
    }

    private void GenerateDistanceVector()
    {
        List<float> absoluteDistanceVector = new List<float>();

        float measureLength = (speed / (BPM/60)) * patternMap.tempoDenominator;
        float baseDistance = 0;
        foreach(BeatPattern bp in patternMap.pattern)
        {
            foreach(float notePosition in bp.notePositions)
            {
                if (notePosition >= 0 && notePosition < bp.numMeasures)
                {
                    absoluteDistanceVector.Add(baseDistance + measureLength * notePosition);
                }
            }
            baseDistance += measureLength * bp.numMeasures;
        }

        absoluteDistanceVector.Sort();

        firstNoteDistance = absoluteDistanceVector[0] + patternMap.initialDelay*speed;

        distanceVector = new List<float>(absoluteDistanceVector.Capacity);
        for (int i = 0; i < absoluteDistanceVector.Count-1; i++)
        {
            distanceVector.Add(absoluteDistanceVector[i + 1] - absoluteDistanceVector[i]);
        }
        distanceVector.Add(absoluteDistanceVector[0] + (baseDistance - absoluteDistanceVector[absoluteDistanceVector.Count - 1]));

        /*print(distanceVector.Count);
        foreach (var a in distanceVector)
            print(a);*/
    }

    private PatternMap GeneratePatternMap()
    {
        //return new PatternMap("default120");
        return JsonUtility.FromJson<PatternMap>(patternMapJSON.text);
    }

    private void GenerateNotes()
    {
        int nNotes = 2*(int)((rectTransform.sizeDelta.x / distanceVector.Min()) + 1);
        noteBufer = new List<GameObject>(nNotes);
        noteScripts = new List<NoteUI>(nNotes);
        noteRectTransforms = new List<RectTransform>(nNotes);

        noteBufferPosition = new Vector2(0, -300);

        for (int i = 0; i < nNotes; i++)
        {
            GameObject note = Instantiate(notePrefab, noteBufferPosition, Quaternion.identity);
            note.transform.SetParent(this.transform, false);
            note.transform.SetSiblingIndex(1);

            noteBufer.Add(note);
            NoteUI script = note.GetComponent<NoteUI>();
            noteScripts.Add(script);
            RectTransform noteRectTransform = note.GetComponent<RectTransform>();
            noteRectTransforms.Add(noteRectTransform);
            script.rectTransform = noteRectTransform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            trySetNote();
        }
    }

    public void startNotes()
    {
        if (!hasStarted)
        {
            if (firstNoteDistance <= rectTransform.sizeDelta.x / 2)
                setNextNotes(rectTransform.sizeDelta.x / 2 - firstNoteDistance);
            else
                StartCoroutine(delayedSetNextNotes((firstNoteDistance - (rectTransform.sizeDelta.x / 2)) / speed));

            trySetNote();
            musicPlayer.Play();
            hasStarted = true;
        }
    }

    private void trySetNote()
    {
        if (settingNextNote)
            return;

        float previousNoteDistance = noteRectTransforms[previousNotes].anchoredPosition.x + (rectTransform.sizeDelta.x / 2);
        if (distanceVector[distanceVectorIndex] <= rectTransform.sizeDelta.x / 2)
        {
            while (previousNoteDistance > distanceVector[distanceVectorIndex])
            {
                setNextNotes(previousNoteDistance - distanceVector[distanceVectorIndex]);
                distanceVectorIndex = (distanceVectorIndex + 1) % distanceVector.Count;
                previousNoteDistance = noteRectTransforms[previousNotes].anchoredPosition.x + (rectTransform.sizeDelta.x / 2);
            }
        }
        else
        {
            StartCoroutine(delayedSetNextNotes((distanceVector[distanceVectorIndex] - previousNoteDistance)/speed));
            distanceVectorIndex = (distanceVectorIndex + 1) % distanceVector.Count;
        }
    }

    private bool settingNextNote = false;
    private IEnumerator delayedSetNextNotes(float delay)
    {
        settingNextNote = true;
        yield return new WaitForSeconds(delay);

        float barWidth = rectTransform.sizeDelta.x;

        //workingNote = noteBufer[nextNotes];
        noteRectTransforms[nextNotes].anchoredPosition = new Vector2(-barWidth / 2, 0);
        noteScripts[nextNotes].speed = speed;

        //workingNote = noteBufer[nextNotes + 1];
        noteRectTransforms[nextNotes + 1].anchoredPosition = new Vector2(barWidth / 2, 0);
        noteScripts[nextNotes + 1].speed = -speed;

        previousNotes = nextNotes;
        nextNotes = (nextNotes + 2) % noteBufer.Count;
        settingNextNote = false;
    }

    private void setNextNotes(float offset)
    {
        //GameObject workingNote;

        float barWidth = rectTransform.sizeDelta.x * 15f/17f;

        //workingNote = noteBufer[nextNotes];
        noteRectTransforms[nextNotes].anchoredPosition = new Vector2(-barWidth / 2 + offset, -5);
        noteScripts[nextNotes].speed = speed;

        //workingNote = noteBufer[nextNotes + 1];
        noteRectTransforms[nextNotes + 1].anchoredPosition = new Vector2(barWidth / 2 - offset, -5);
        noteScripts[nextNotes + 1].speed = -speed;

        previousNotes = nextNotes;
        nextNotes = (nextNotes + 2) % noteBufer.Count;
    }
}
