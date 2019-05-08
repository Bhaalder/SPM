using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    public bool spawnTrigger;
    public bool waitTrigger;
    public Transform EnemyPrefab;

    void Start()
    {
        spawnTrigger = false;
        waitTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTrigger & waitTrigger) {
            spawnTrigger = false;
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
