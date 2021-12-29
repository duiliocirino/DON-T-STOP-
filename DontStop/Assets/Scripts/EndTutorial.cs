using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    private BoxCollider thisCollider;
    public GameplayController gameplayController;

    private void Awake()
    {
        thisCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            thisCollider.enabled = false;
            gameplayController.UnlockNextStage();
            gameplayController.SaveData();
        }
    }
}
