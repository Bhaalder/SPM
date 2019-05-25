﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour{
    //Author: Patrik Ahlgren

    [Header("Movementspeeds")]
    [SerializeField] private float movementSpeed = 14;
    [SerializeField] private float speedMultiplier; //denna används för att öka movmentspeed med pickups, per 0,1 ökas 10%

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float extraJumps = 1;
    [SerializeField] private float fakeExtraGravity = 15;
    [Tooltip("The chance to play a jumpgrunt sound 1-100")]
    [SerializeField] private float jumpSoundPercentChance = 25;

    [Header("Dash")]
    [SerializeField] private float dashForce = 20;
    [SerializeField] private float nextTimeToDash = 2;
    [SerializeField] private float dashDuration = 0.5f;

    private float timeToDash;
    private bool isDashing;
    private float jumpCount;
    private float distanceToGround;   

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private BoxCollider groundCheck;
    private Vector2 velocity;

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
        if (!isDashing) {
            Move();
        }                    
        FakeExtraGravity();
        if (Time.time <= timeToDash) {
            rigidBody.velocity = new Vector3(0, 0, 0);
            transform.position += (Camera.main.transform.forward * (dashForce * (1+speedMultiplier))) * Time.deltaTime;
        } else {
            isDashing = false;
        }
    }

    private void Move() {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isIdle = movementInput.magnitude == 0;
        if (IsGrounded() && !isIdle) {
            AudioController.Instance.PlaySFX_RandomPitchAndVolume_Finish("Walking", 0.9f, 1f, 0);
            //Debug.Log("Walking!");
        } else {
        }
        movementInput *= (movementSpeed * (1 + speedMultiplier)) * Time.deltaTime;

        velocity = movementInput;

        transform.Translate(new Vector3(velocity.x, 0f, velocity.y));
    }


    private void Jump() {
        if (Input.GetButtonDown("Jump")) {           
            if (jumpCount>0 || IsGrounded()) {
                JumpSound();
                jumpCount--;
                if(rigidBody.velocity.y > 0) {
                    rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
                } else {
                    rigidBody.velocity = new Vector3(0, 0, 0);
                }               
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            IsGrounded();
        }
    }

    private void JumpSound() {
        int i = Random.Range(1, 5);
        int soundChance = Random.Range(1, 100);
        if (soundChance <= jumpSoundPercentChance) {
            switch (i) {
                case 1:
                    AudioController.Instance.PlaySFX_RandomPitch("Jump1", 0.95f, 1f);
                    break;
                case 2:
                    AudioController.Instance.PlaySFX_RandomPitch("Jump2", 0.95f, 1f);
                    break;
                case 3:
                    AudioController.Instance.PlaySFX_RandomPitch("Jump3", 0.95f, 1f);
                    break;
                default:
                    Debug.LogWarning("JumpSound did not execute correctly, i out of range");
                    break;
            }
        }     
    }

    public void Dash() {
        isDashing = true;
        if(Time.time >= timeToDash+nextTimeToDash) {
            AudioController.Instance.PlaySFX_RandomPitch("Dash", 0.93f, 1f);
            timeToDash = Time.time + dashDuration;
        }
    }

    public void SpeedMultiplier(float speedDuration, float speedChange) {
        StartCoroutine(SpeedChange(speedDuration, speedChange));
    }

    private IEnumerator SpeedChange(float speedDuration, float speedChange) {
        speedMultiplier = speedChange;
        yield return new WaitForSeconds(speedDuration);
        speedMultiplier = 0;
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
