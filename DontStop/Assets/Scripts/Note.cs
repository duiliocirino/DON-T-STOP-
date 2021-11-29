using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private NotesHandler _notesHandler;
    [SerializeField] protected float noteLife = 5f;
    protected float timeOn = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _notesHandler = GameObject.FindWithTag("NotesHandler").GetComponent<NotesHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        timeOn += Time.deltaTime;
        if (timeOn >= noteLife)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("You collided with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            _notesHandler.NoteTaken();
            Destroy(gameObject);
        }
    }
}
