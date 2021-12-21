using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPlatformTutorial : MonoBehaviour
{

    private bool alreadyShown = false;
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
        if (!alreadyShown)
        {
            if (other.tag.Equals("Player"))
            {
                //TutorialController.instance.showDialogBox("BadPlatformTutorial", 10);
                alreadyShown = true;
            }
        }
    }
}
