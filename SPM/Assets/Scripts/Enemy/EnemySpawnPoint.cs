using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    public bool spawntrigger;
    public bool waitTrigger;
    public Transform EnemyPrefab;

    void Start()
    {
        spawntrigger = false;
        waitTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawntrigger & waitTrigger) {
            spawntrigger = false;
            waitTrigger = false;
            StartCoroutine(Spawntimer());
        }

    }

    IEnumerator Spawntimer()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        waitTrigger = true;
    }
}
