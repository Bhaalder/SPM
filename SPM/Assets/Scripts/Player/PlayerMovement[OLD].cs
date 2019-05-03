using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateHandler;

public class PlayerMovement : MonoBehaviour
{
    private bool controlingCam = true; // ändrat

    private float gravity = 9.82f;
    private float xRotation, yRotation;
    [SerializeField] private float rotationSpeedX, rotationSpeedY;

    [SerializeField] private float movementSpeed;

    private CameraFollow cam;

    [SerializeField] private GameObject camFocus;

    private Rigidbody rbPlayer;

    public PlayerStateMachine<PlayerMovement> playerStateMachine { get; set; }

    private PlayerCollision playerColl;
    public PlayerCollision GetPlayerColl() { return playerColl; }

    private float speedBoost = 0;
    public float speedBoostTime = 10;

    private void Start()
    {
        playerStateMachine = new PlayerStateMachine<PlayerMovement>(this);
        playerStateMachine.ChangeState(PlayerOnGroundState.Instance);
        cam = GameObject.Find("MainCamera").GetComponent<CameraFollow>();
        rbPlayer = GetComponent<Rigidbody>();
        playerColl = GetComponent<PlayerCollision>();
    }

    private void Update()
    {
        RotatePlayerAndCam();
    }

    private void FixedUpdate()
    {
        playerStateMachine.Update();
    }

    public void Jump()
    {
        rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, 8, rbPlayer.velocity.z);
    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.W) && !playerColl.GetFrontColl())
            transform.position += playerColl.GetForward() * (movementSpeed + speedBoost) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && !playerColl.GetBackColl())
            transform.position -= playerColl.GetForward() * (movementSpeed + speedBoost) * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) && !playerColl.GetLeftColl())
            transform.position -= playerColl.GetRight() * (movementSpeed + speedBoost) * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) && !playerColl.GetRightColl())
            transform.position += playerColl.GetRight() * (movementSpeed + speedBoost) * Time.deltaTime;

        rbPlayer.velocity = new Vector3(0, rbPlayer.velocity.y, 0);
    }

    private void AddAdditionalMovement()
    {
        transform.position += playerColl.GetAdditionalMovement();
    }

    private void RotatePlayerAndCam()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (!controlingCam)
        //        controlingCam = true;
        //    else
        //        controlingCam = false;
        //}
        //Högerklick för att röra crosshair + kamera
        if (controlingCam)
        {
            xRotation += Input.GetAxis("Mouse X");
            yRotation -= Input.GetAxis("Mouse Y");
            if (yRotation >= 30)
                yRotation = 30;
            else if (yRotation <= -30)
                yRotation = -30;
            transform.rotation = Quaternion.Euler(0, xRotation * rotationSpeedX, 0);
            cam.SetCurrentY(-xRotation * rotationSpeedX);
            cam.SetCurrentX(-yRotation * rotationSpeedY);
            camFocus.GetComponent<CameraFocus>().SetCurrentX(yRotation * rotationSpeedY);
            camFocus.GetComponent<CameraFocus>().SetCurrentY(xRotation * rotationSpeedX);
        }
    }

    public void Knock(Vector3 force)
    {
        rbPlayer.velocity = force;
    }

    public void ApplyGravity()
    {
        rbPlayer.velocity += new Vector3(0, -gravity * Time.deltaTime, 0);
    }

    public void StartSpeedBoost(float speed, float speedTime)
    {
        speedBoost = speed;
        StartCoroutine(EndSpeedBoost(speedTime));
    }

    IEnumerator EndSpeedBoost(float speedTime) {
        yield return new WaitForSeconds(speedTime);
        speedBoost = 0;
    }

}
