using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public Rigidbody rb;

    public float speed = 10;
    public float runSpeed = 15;
    public float jumpForce = 200;

    private bool _isGrounded;

    void Start () 
    {
        inputManager.inputMaster.Move.Jump.started += _ => Jump();
    }



     void Update()
    {
       float forward = inputManager.inputMaster.Move.Forward.ReadValue<float>();
       float right = inputManager.inputMaster.Move.Right.ReadValue<float>();
       Vector3 move = transform.right * right + transform.forward * forward;

       move *= inputManager.inputMaster.Move.Run.ReadValue<float>() == 0 ? speed : runSpeed;
       transform.localScale = new Vector3(1, inputManager.inputMaster.Move.Crouch.ReadValue<float>() == 0 ? 1f : 0.72618f, 1);
       

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        _isGrounded = true;
    }


    void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Ground"))

        _isGrounded = false;
    }
    void Jump()
    {
        if (_isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

    }

    


}
