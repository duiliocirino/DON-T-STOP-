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
    public Animator pulsatingKeyAnimator;

    public static RhythmControllerUI instance { get; private set; }
    public bool hasStarted = false;
    public Vector2 noteBufferPosition;
    public bool noteInHitArea = false;
    public float noteDespawnDelay;

    public PatternMap patternMap;

    private float speed;
    private float BPM;
    private float firstNoteTime;
    private List<float> timeVector;
    private int timeVectorIndex = 0;

    private RectTransform rectTransform;

    private List<GameObject> noteBufer;
    private List<NoteUI> noteScripts;
    private List<RectTransform> noteRectTransforms;
    private int nextNotes = 0;
    private bool pulsatingPlayed;

    public float usableBarFraction = 15f / 17f;
    private float barWidth;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
        barWidth = rectTransform.sizeDelta.x * usableBarFraction;

        pulsatingKeyAnimator = hitZone.GetComponent<Animator>();

        patternMap = GeneratePatternMap();

        speed = patternMap.noteSpeed;
        BPM = patternMap.BPM;
        timeVectorIndex = patternMap.numberOfNotesSkippedOnFirstPlay;
        GenerateTimeVector();

        GenerateNotes();

        GenerateHitZones();
    }

    private PatternMap GeneratePatternMap()
    {
        //return new PatternMap("default120");
        return JsonUtility.FromJson<PatternMap>(patternMapJSON.text);
    }

    private void GenerateTimeVector()
    {
        timeVector = new List<float>();
        float noteTime = (60 / BPM);
        float measureTime = noteTime * patternMap.tempoNumerator;
        float baseTime = 0;
        foreach (BeatPattern bp in patternMap.pattern)
        {
            foreach (float notePosition in bp.notePositions)
            {
                if (notePosition >= 0 && notePosition < bp.numMeasures)
                {
                    timeVector.Add(baseTime + measureTime * notePosition);
                }
            }
            float thisMeasureTime = measureTime;
            if (bp.tempoNumerator != default && bp.tempoDenominator != default && bp.tempoNumerator != patternMap.tempoNumerator && bp.tempoDenominator != patternMap.tempoDenominator)
                thisMeasureTime = noteTime * bp.tempoNumerator;
        baseTime += thisMeasureTime * bp.numMeasures;
        }

        timeVector.Sort();

        firstNoteTime = timeVector[patternMap.numberOfNotesSkippedOnFirstPlay] + patternMap.initialDelay;

        //print(timeVector.Count);
        //foreach (var a in timeVector)
        //print(a);
    }

    private void GenerateNotes()
    {
        int nTimes = timeVector.Count;
        //Debug.Log(nTimes);
        List<float> timeVectorDifferences = new List<float>(nTimes);
        for (int i = 0; i < nTimes - 1; i++)
        {
            timeVectorDifferences.Add(timeVector[i + 1] - timeVector[i]);
        }
        if (musicPlayer.clip.length - timeVector[nTimes - 1] >= 60 / BPM)
        {
            timeVectorDifferences.Add(musicPlayer.clip.length - timeVector[nTimes - 1] + timeVector[0]);
        }
        else
        {
            timeVectorDifferences.Add(timeVectorDifferences[nTimes - 2]);
        }

        //foreach (float f in timeVectorDifferences)
        //Debug.Log(f);

        int nNotes = 2 * (int)((barWidth / (timeVectorDifferences.Min() * speed)) + 1);
        //Debug.Log(nNotes);

        noteBufer = new List<GameObject>(nNotes);
        noteScripts = new List<NoteUI>(nNotes);
        noteRectTransforms = new List<RectTransform>(nNotes);

        noteBufferPosition = new Vector2(0, -300);


        for (int i = 0; i < nNotes; i++)
        {
            GameObject note = Instantiate(notePrefab, noteBufferPosition, Quaternion.identity);
            note.transform.SetParent(this.transform, false);
            note.transform.SetSiblingIndex(0);

            noteBufer.Add(note);
            NoteUI script = note.GetComponent<NoteUI>();
            noteScripts.Add(script);
            RectTransform noteRectTransform = note.GetComponent<RectTransform>();
            noteRectTransforms.Add(noteRectTransform);
            script.rectTransform = noteRectTransform;
            script.musicPlayer = musicPlayer;
        }
    }

    private void GenerateHitZones()
    {
        float noteLength = speed * (60 / BPM);

        hitZone.size = new Vector2(noteLength * hitTime, 50);

        noteDespawnDelay = (60 / BPM) * hitTime;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    float previousTime = -1;
    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            float musicTime = musicPlayer.time;

            if (!noteInHitArea && pulsatingPlayed)
                pulsatingPlayed = false;
            if (noteInHitArea && !pulsatingPlayed)
            {
                pulsatingKeyAnimator.Play("PulsatingKey");
                pulsatingPlayed = true;
            }

            if (musicTime < previousTime)
            {
                timeVectorIndex = 0;
            }
            previousTime = musicTime;

            if (timeVectorIndex < timeVector.Count && timeVector[timeVectorIndex] - (barWidth / 2) / speed + patternMap.initialDelay < musicTime)
            {
                setNextNotes();
            }
        }
    }

    public void StartNotes()
    {
        if (!hasStarted)
        {
            if (firstNoteTime * speed <= barWidth / 2)
            {
                noteRectTransforms[nextNotes].anchoredPosition = new Vector2(-firstNoteTime * speed, -5);
                noteScripts[nextNotes].speed = speed;
                noteScripts[nextNotes].timeToReachCenter = firstNoteTime;

                noteRectTransforms[nextNotes + 1].anchoredPosition = new Vector2(firstNoteTime * speed, -5);
                noteScripts[nextNotes + 1].speed = -speed;
                noteScripts[nextNotes + 1].timeToReachCenter = firstNoteTime;

                timeVectorIndex++;
            }

            musicPlayer.Play();
            hasStarted = true;
        }
    }

    private void setNextNotes()
    {
        if (timeVectorIndex >= timeVector.Count) return;

        //Debug.Log("setting note " + timeVectorIndex + " in position " + new Vector2(-(timeVector[timeVectorIndex] - musicPlayer.time + patternMap.initialDelay) * speed, -5) + " at time " + musicPlayer.time);
        noteRectTransforms[nextNotes].anchoredPosition = new Vector2(-(timeVector[timeVectorIndex] - musicPlayer.time + patternMap.initialDelay) * speed, -5);
        noteScripts[nextNotes].speed = speed;
        noteScripts[nextNotes].timeToReachCenter = timeVector[timeVectorIndex];

        noteRectTransforms[nextNotes + 1].anchoredPosition = new Vector2((timeVector[timeVectorIndex] - musicPlayer.time + patternMap.initialDelay) * speed, -5);
        noteScripts[nextNotes + 1].speed = -speed;
        noteScripts[nextNotes + 1].timeToReachCenter = timeVector[timeVectorIndex];

        nextNotes = (nextNotes + 2) % noteBufer.Count;

        timeVectorIndex++;
    }
}
