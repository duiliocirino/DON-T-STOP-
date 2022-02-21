using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJump : MonoBehaviour
{
    public float ySppedTreshold = 0;
    public float force = 2800;
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
        if (other.relativeVelocity.y <= -ySppedTreshold || other.relativeVelocity.y >= ySppedTreshold)
        {
            other.gameObject.GetComponent<Animator>().SetBool("OnTrampoline", true);
            other.rigidbody.AddForce(Vector3.up * force);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        other.gameObject.GetComponent<Animator>().SetBool("OnTrampoline", false);
    }
}
