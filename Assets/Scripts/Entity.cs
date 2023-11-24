using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Team
{
    None,
    Player,
    Enemy,
}

public class Entity : MonoBehaviour
{
    [Header("Entity")]
    public Team team;
    public Collider coll;
    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    public float maxHealth = 3f;
    public float health;
    public float damage = 1f;
    [Range(0, 1)]
    public float defense = 1f;
    public UnityAction<Entity> onDie;

    public void SetHealth()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage * defense);
        if (health == 0)
            Die();
    }

    public void Kill()
    {
        Die();
    }

    protected void Die()
    {
        onDie?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var opponent = other.gameObject.GetComponent<Entity>();
        if (opponent != null && opponent.team != team)
            opponent.TakeDamage(damage);
    }
}
