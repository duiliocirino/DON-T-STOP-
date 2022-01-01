using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    private BoxCollider thisCollider;
    public GameplayController gameplayController;
    public GameObject background;
    public GameObject endOfTutorialUI;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            thisCollider.enabled = false;
            gameplayController.SetPlayerControlActive(false);
            LifeBar.instance.StopDeplition();
            gameplayController.UnlockNextStage();
            gameplayController.SaveData();
            background.SetActive(true);
            endOfTutorialUI.SetActive(true);
        }
    }

    public void ResumeTutorialStage()
    {
        endOfTutorialUI.SetActive(false);
        background.SetActive(false);
        LifeBar.instance.StartDeplition();
        gameplayController.SetPlayerControlActive(true);
    }
}
