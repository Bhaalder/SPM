﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movementspeeds")]
    public float movementSpeed = 12;
    public float speedMultiplier; //denna används för att öka movmentspeed med pickups, per 0,1 ökas 10%

    [Header("Jumping")]
    public float jumpForce = 10;
    public float extraJumps;
    public float fakeExtraGravity = 5;

    [Header("Dash")]//denna funktion funkar ej än, känns dåligt
    public float dashForce = 10;
    public float nextTimeToDash = 2;
    public float dashDuration = 0.5f;

    private float timeToDash;
    private bool isDashing;
    private float jumpCount;
    private float distanceToGround;   

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private BoxCollider groundCheck;
    private Vector2 velocity;

    //public LayerMask layerMask;
    //private RaycastHit raycastHit;
    //public float skinWidth = 0.5f;

    void Start(){
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        groundCheck = GetComponent<BoxCollider>();
        distanceToGround = groundCheck.bounds.extents.y;
    }

    private void Update() {
        Jump();

    }
    private void FixedUpdate(){
        Move();                
        FakeExtraGravity();
        if (Time.time <= timeToDash) {
            transform.position += (transform.forward * dashForce) * Time.deltaTime;
        }
    }

    private void Move() {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput *= (movementSpeed*(1+speedMultiplier)) * Time.deltaTime;

        velocity = movementInput;

        //float rayLength = velocity.magnitude + skinWidth;
        //bool hit = Physics.BoxCast(transform.position, capsuleCollider.ClosestPointOnBounds(transform.position), transform.forward, out raycastHit, transform.rotation, rayLength, layerMask);
        //if (hit) {
        //    velocity = (raycastHit.distance - skinWidth) * velocity.normalized;
        //    rayLength = raycastHit.distance;
        //    Debug.Log("Kolliderar med " + raycastHit.collider.name);
        //}

        transform.Translate(new Vector3(velocity.x, 0f, velocity.y));
    }
    private void Jump() {
        if (Input.GetButtonDown("Jump")) {          
            if (jumpCount>0 || IsGrounded()) {
                jumpCount--;
                velocity.y = 0;
                //rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                rigidBody.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            IsGrounded();
        }
    }

    public void Dash() {
        isDashing = true;
        if(Time.time >= timeToDash+nextTimeToDash) {
            timeToDash = Time.time + dashDuration;
        }
    }

    private void FakeExtraGravity() {
        if (!IsGrounded()) {
            rigidBody.velocity += new Vector3(0, -fakeExtraGravity * Time.deltaTime, 0);
        }      
    }

    private bool IsGrounded() {
        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f)) {           
            jumpCount = extraJumps;
            return true;
        } else return false;
    }
}
