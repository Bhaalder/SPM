using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour{
    //Author: Patrik Ahlgren

    [Header("Movementspeeds")]
    public float movementSpeed = 12;
    public float speedMultiplier; //denna används för att öka movmentspeed med pickups, per 0,1 ökas 10%

    [Header("Jumping")]
    public float jumpForce = 10;
    public float extraJumps;
    public float fakeExtraGravity = 5;

    [Header("Dash")]
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
            transform.position += (Camera.main.transform.forward * (dashForce * (1+speedMultiplier))) * Time.deltaTime;
        } else {
            isDashing = false;
        }
    }

    private void Move() {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput *= (movementSpeed*(1+speedMultiplier)) * Time.deltaTime;

        velocity = movementInput;

        transform.Translate(new Vector3(velocity.x, 0f, velocity.y));
    }
    private void Jump() {
        if (Input.GetButtonDown("Jump")) {          
            if (jumpCount>0 || IsGrounded()) {
                jumpCount--;
                velocity.y = 0;
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
