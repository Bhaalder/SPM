using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float movementSpeed;
    public float jumpForce;
    public float fakeExtraGravity;

    private Rigidbody rigidBody;
    
    public float skinWidth = 0.5f;

    public LayerMask layerMask;

    CapsuleCollider capsuleCollider;
    //BoxCollider boxCollider;//
    RaycastHit raycastHit;


    void Start(){

        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //boxCollider = GetComponent<BoxCollider>();//
    }

    void FixedUpdate(){
        Move();
        Jump();         
        FakeExtraGravity();
    }

    private void Move() {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput *= movementSpeed * Time.deltaTime;

        Vector2 velocity = movementInput;

        float rayLength = velocity.magnitude + skinWidth;
        bool hit = Physics.BoxCast(transform.position, capsuleCollider.ClosestPointOnBounds(transform.position), transform.forward, out raycastHit, transform.rotation, rayLength, layerMask);
        //bool hit = Physics.BoxCast(transform.position, boxCollider.size, transform.forward, out raycastHit, transform.rotation, rayLength, layerMask);
        if (hit) {
            velocity = (raycastHit.distance - skinWidth) * velocity.normalized;
            rayLength = raycastHit.distance;
        }

        transform.Translate(new Vector3(velocity.x, 0f, velocity.y));
    }
    private void Jump() {
        if (Input.GetButtonDown("Jump")) {
            if (IsGrounded()) {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void FakeExtraGravity() {
        if (!IsGrounded()) {
            rigidBody.velocity += new Vector3(0, -fakeExtraGravity * Time.deltaTime, 0);
        }      
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }
}
