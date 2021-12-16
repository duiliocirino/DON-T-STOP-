using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class JoinController : MonoBehaviour
{
    [SerializeField] GameObject runnerJoin;
    [SerializeField] GameObject creatorJoin;
    [SerializeField] GameObject creatorJoined;
    [SerializeField] GameObject runnerJoined;
    [SerializeField] Button StartButton;
    private Rect bounds;
    private void Start()
    {

        bounds = new Rect(Screen.width / 1.7f, 0, Screen.width / 2, Screen.height);
        StartButton.interactable = false;
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            PlayerJoin(1);
        }

            
            if (Input.GetMouseButtonDown(0) && bounds.Contains(Input.mousePosition))
            {
                PlayerJoin(2);
            }
        

    }
    void PlayerJoin(int player)
    {
        if (player == 1){
            runnerJoin.SetActive(false);
            runnerJoined.SetActive(true);
            if (creatorJoined.activeSelf)
            {
                StartButton.interactable = true;
            }
        }
            
        if (player == 2){
            creatorJoin.SetActive(false);
            creatorJoined.SetActive(true);
            if (runnerJoined.activeSelf)
            {
                StartButton.interactable = true;
            }
        }
            

    }
}
