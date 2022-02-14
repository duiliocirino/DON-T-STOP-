using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Lights : MonoBehaviour
{
    [SerializeField]
    List<GameObject> spotLightsRight;
    [SerializeField]
    List<GameObject> spotLightsLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject spotLight in spotLightsRight)
            spotLight.transform.Rotate(0, 0.8f, 0);
        foreach (GameObject spotLight in spotLightsLeft)
            spotLight.transform.Rotate(0, -0.8f, 0);
    }
}
