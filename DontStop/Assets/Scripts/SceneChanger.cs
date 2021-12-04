using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
             
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 5)
            SceneManager.LoadScene("MainMenu");
    }
}
