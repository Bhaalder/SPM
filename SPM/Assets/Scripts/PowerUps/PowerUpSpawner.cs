using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public Transform[] powerUp = new Transform[5];
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(powerUp[1], transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawner(int powerUpNumber) {
        Debug.Log("starting respawner");
        StartCoroutine(Spawntimer(powerUpNumber));
    }

    IEnumerator Spawntimer(int powerUpNumber)
    {
        yield return new WaitForSeconds(5f);

        Instantiate(powerUp[powerUpNumber], transform.position, Quaternion.identity);        
    }
}
