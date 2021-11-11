using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float movementSpeed = 200.0f;
    public float jumpForce = 30.0f;
    
    private Rigidbody rb;

    private float movementX;
    private float movementY;
    private float movementZ;

    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody> ();
    }
    void FixedUpdate ()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementZ);
        rb.AddForce(movement * movementSpeed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
 
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
