using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float movementSpeed;
    public float speedMultiplier; //denna används för att öka movmentspeed med pickups, per 0,1 ökas 10%
    public float jumpForce;
    public float extraJumps;
    public float fakeExtraGravity;

    private float jumpCount;
    private Rigidbody rigidBody;
    
    private CapsuleCollider capsuleCollider;

    //public LayerMask layerMask;
    //private RaycastHit raycastHit;
    //public float skinWidth = 0.5f;

    void Start(){
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void FixedUpdate(){
        Jump();
        Move();                
        FakeExtraGravity();
    }

    private void Move() {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput *= (movementSpeed*(1+speedMultiplier)) * Time.deltaTime;

        Vector2 velocity = movementInput;

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
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            IsGrounded();
        }
    }

    private void FakeExtraGravity() {
        if (!IsGrounded()) {
            rigidBody.velocity += new Vector3(0, -fakeExtraGravity * Time.deltaTime, 0);
        }      
    }

    private bool IsGrounded() {
        if (Physics.Raycast(transform.position, Vector3.down, 0.75f)) {
            jumpCount = extraJumps;
            return true;
        } else return false;
    }
}
