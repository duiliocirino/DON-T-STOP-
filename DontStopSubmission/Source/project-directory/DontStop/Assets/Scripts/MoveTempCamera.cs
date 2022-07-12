using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTempCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, 0.35f, 0.2f));
    }
}
