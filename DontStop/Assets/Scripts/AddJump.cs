using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJump : MonoBehaviour
{
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
        other.gameObject.GetComponent<Animator>().SetBool("OnTrampoline", true);
        other.rigidbody.AddForce(Vector3.up * 2800);
    }

    private void OnCollisionExit(Collision other)
    {
        other.gameObject.GetComponent<Animator>().SetBool("OnTrampoline", false);
    }
}
