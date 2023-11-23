using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TestScript : MonoBehaviour
{
    private SkinnedMeshRenderer SMR;
    private MeshCollider MeshCollider;

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
}