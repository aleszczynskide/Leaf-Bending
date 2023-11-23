using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int PlayerSpeed, JumpForce;  //Player Speed Int and jumppower
    Rigidbody Rb;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }
    void Update()

    {
        //Player Movement Script
        Vector3 MoveDirection = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) MoveDirection.z = -1;
        if (Input.GetKey(KeyCode.S)) MoveDirection.z = +1;
        if (Input.GetKey(KeyCode.A)) MoveDirection.x = +1;
        if (Input.GetKey(KeyCode.D)) MoveDirection.x = -1;
        if (Input.GetKeyDown(KeyCode.Space)) Rb.velocity = new Vector3(0, +JumpForce, 0);

        transform.position += MoveDirection * PlayerSpeed * Time.deltaTime;


    }
}
