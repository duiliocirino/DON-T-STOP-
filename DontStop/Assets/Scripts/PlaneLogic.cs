using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLogic : MonoBehaviour
{
    public int platformSize = 1;
    public bool isPlayerOn = false;
    [SerializeField] public float planeLife = 5f;
    [SerializeField] protected float missPenalty = 0.5f;
    [SerializeField] ParticleSystem fallingPlatformParticles;
    protected float timeOn = 0f;
    public Vector3 initialPosition;
    public Color preview;
    public Sprite imagePreview;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOn)
        {
            timeOn += Time.deltaTime;
        }

        if (timeOn < planeLife && timeOn >= planeLife * 0.78f && isPlayerOn)
        {
            TutorialController.instance.firstFall = true;
        }
        if (timeOn < planeLife && timeOn >= planeLife * 0.8f && isPlayerOn)
        {
            fallingPlatformParticles.Play();
            ShakePlatform();
        }
        if (timeOn >= planeLife)
        {

            //WaitForParticles();  
            PlaneHandler.instance.DisablePlatform(gameObject);
        }
    }
    IEnumerator WaitForParticles()
    {      
        fallingPlatformParticles.Play();
    
        yield return new WaitForSeconds(fallingPlatformParticles.main.duration);

    }

    void ShakePlatform()
    {
        if (Time.timeScale != 0) gameObject.transform.position += new Vector3(Mathf.Sin(50 * (timeOn - planeLife * 0.8f)), 0, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        isPlayerOn = true;
    }

    private void OnCollisionStay(Collision other)
    {
        isPlayerOn = true;
    }

    private void OnCollisionExit(Collision other)
    {
        isPlayerOn = false;
        gameObject.transform.position = initialPosition;
    }
}