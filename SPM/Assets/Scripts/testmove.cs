using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmove : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        transform.Translate(speed * moveHorizontal * Time.deltaTime, 0f, speed * moveVertical * Time.deltaTime);
    }
}
