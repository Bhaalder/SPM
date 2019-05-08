using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    //Author: Teo


    // Update is called once per frame
    void Update () {

        transform.Rotate(new Vector3(0, 0, 50) * Time.deltaTime);

	}
}
