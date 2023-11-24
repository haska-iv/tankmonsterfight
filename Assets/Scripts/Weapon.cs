using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float damage;

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.damage = this.damage;
        bullet.enabled = true;
        bullet.onDie += OnBulletDie;
    }

    private void OnBulletDie(Entity bullet)
    {
        bullet.onDie -= OnBulletDie;
        Destroy(bullet.gameObject);
    }
}
