using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Player")]
    public Weapon gun;
    public Weapon machineGun;

    private Weapon activeGun;
    private Vector3 direction;

    private void Awake()
    {
        damage = 0;
        activeGun = gun;
        direction = Vector3.zero;
    }   

    private void Update()
    {
        float verticalInput = 0f;
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
            verticalInput = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            verticalInput = - 1;

        if (Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = 1;
        else if (Input.GetKey(KeyCode.RightArrow))
            horizontalInput = - 1;

        Move(verticalInput, horizontalInput);

        if (Input.GetKeyDown(KeyCode.Q))
            ChangeWeapon(gun);
        else if (Input.GetKeyDown(KeyCode.W))
            ChangeWeapon(machineGun);

        if (Input.GetKeyDown(KeyCode.X))
            Shoot();
    }

    private void Move(float verticalInput, float horizontalInput)
    {
        transform.Translate(verticalInput * Vector3.forward * Time.deltaTime * moveSpeed); 
        var binaryRotation = verticalInput < 0 ? horizontalInput : horizontalInput * -1;
        transform.Rotate(binaryRotation * Vector3.up * Time.deltaTime * rotationSpeed);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        activeGun = weapon;
    }

    void Shoot()
    {
        activeGun.Shoot();
    }
}
