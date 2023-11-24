using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int PlayerSpeed, JumpForce;  //Player Speed Int and jumppower
    Rigidbody Rb;
    private GameObject Leaf;
    RaycastHit Hit;
    public float Distance;
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
        if (Physics.Raycast(transform.position, Vector3.down, out Hit, Distance))
        {
            if (Hit.collider.CompareTag("Leaf"))
            {
                Debug.Log("Li��");
                Leaf = Hit.collider.gameObject;
                Leaf.GetComponent<TestScript>().Player = this.gameObject;
                this.transform.SetParent(Leaf.GetComponent<TestScript>().Bone1.transform);
            }
            else
            {
                if (Leaf != null)
                {
                    Leaf.GetComponent<TestScript>().Player = null;
                    Leaf = null;
                }
            }
        }
        else
        {
            if (Leaf != null)
            {
                Leaf.GetComponent<TestScript>().Player = null;
                Leaf = null;
            }
        }
    }
}
