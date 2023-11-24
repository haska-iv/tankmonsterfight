using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Enemy")]
    public Transform model;

    private Transform target;
    private float angle;
    private Vector3 angles;

    private void Awake()
    {
        angles = transform.localEulerAngles;
    }
    private void Update()
    {
        if (target != null)
        {
            Rotate();
            Move();
        }
    }

    private void Rotate()
    {
        var angleVelocity = 0f;
        var direction = transform.position - target.position;
        var targetAngle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref angleVelocity, 0.01f, rotationSpeed);
        angles.y = angle;
        model.localEulerAngles = angles;
    }

    private void Move()
    {
        var direction = (target.position - transform.position).normalized;
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
