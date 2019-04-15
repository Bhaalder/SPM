using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempExplosion : MonoBehaviour
{
    private float timer = 0.1f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
            Destroy(gameObject);
    }
}
