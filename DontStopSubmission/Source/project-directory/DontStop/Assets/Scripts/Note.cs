using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private NotesHandler _notesHandler;
    [SerializeField] protected float noteLife = 15f;
    protected float timeOn = 0f;
    public float firstBlink = 10;
    public int value = 1;
    public float secondBlink = 20;
    private Material _material;
    private Outline _outline;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _notesHandler = GameObject.FindWithTag("NotesHandler").GetComponent<NotesHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        timeOn += Time.deltaTime;
        if (timeOn >= 0.7f * noteLife && timeOn <= 0.9f * noteLife) Flashing(firstBlink, 0.7f);
        if (timeOn > 0.9f * noteLife && timeOn <= noteLife) Flashing(secondBlink, 0.9f);
        if (timeOn >= noteLife)
        {
            _notesHandler.NoteNotTaken(value);
            Destroy(gameObject);
        }
    }

    private void Flashing(float frequency, float zeroTime)
    {
        Color color = _material.color;
        color.a = Mathf.Cos(frequency * (timeOn - noteLife * zeroTime));
        _material.color = color;
        _outline.OutlineWidth = Mathf.Abs(4.5f * Mathf.Cos(frequency * (timeOn - noteLife * zeroTime)));
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _notesHandler.NoteTaken();
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _notesHandler.NoteTaken(value);
            Destroy(gameObject);
        }
    }
}
