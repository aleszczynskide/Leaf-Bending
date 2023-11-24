using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TestScript : MonoBehaviour
{
    private SkinnedMeshRenderer SMR;
    private MeshCollider MeshCollider;
    [SerializeField] public GameObject Pointer, Player;
    [SerializeField] public GameObject Bone1, Bone2, Bone3, Bone4, Bone5, Bone6;
    private float Distance;

    void Start()
    {
        SMR = GetComponent<SkinnedMeshRenderer>();
        MeshCollider = GetComponent<MeshCollider>();

        if (SMR != null && MeshCollider != null)
        {
            MeshCollider.sharedMesh = SMR.sharedMesh;
        }
        else
        {
            Debug.Log("Pad³o");
        }
    }

    void Update()
    {
        UpdateMeshCollider();
    }

    void UpdateMeshCollider()
    {
        if (SMR != null && MeshCollider != null)
        {
            Mesh mesh = new Mesh();
            SMR.BakeMesh(mesh);
            MeshCollider.sharedMesh = mesh;
        }
    }
    private void LeafBendingByWalking()
    {
        RotateBones(Bone1);
        RotateBones(Bone2);
        RotateBones(Bone3);
        RotateBones(Bone4);
        RotateBones(Bone5);
        RotateBones(Bone6);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject;
            Player.transform.SetParent(Bone6.transform);
            LeafBendingByWalking();
        }
    }

    private void RotateBones(GameObject Bone)
    {
        Distance = (Pointer.transform.position.x - Player.transform.position.x);

        if (Distance < -2)
        {
            float RotationSpeed = 3.0f;
            float TargetRotationAngle = Mathf.Atan2(Distance, 1) * 1.2f * Mathf.Rad2Deg;
            float CurrentRotation = Bone.transform.rotation.eulerAngles.z;
            float NewRotation = Mathf.LerpAngle(CurrentRotation, TargetRotationAngle, RotationSpeed * Time.deltaTime);
            Bone.transform.rotation = Quaternion.Euler(0, 0, NewRotation);
        }

        Debug.Log(Distance);
    }
}