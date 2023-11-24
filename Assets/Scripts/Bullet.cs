using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private float timer = 5f;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (timer < 0)
            Die();
    }
}
