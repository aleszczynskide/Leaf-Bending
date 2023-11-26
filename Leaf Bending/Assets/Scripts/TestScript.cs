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
    public GameObject Pointer, Player;
    public GameObject Bone1, Bone2, Bone3, Bone4, Bone5, Bone6;
    private float Distance;
    public float MaxValue;
    private float MinValue;
    public float Landed;
    public bool IsRotating;
    private int TimesOfBending;
    private bool IsNegative;

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
            Debug.Log("Pad�o");
        }
    }

    void Update()
    {
        if (Player != null)
        {
            Distance = Mathf.Abs(Pointer.transform.position.x - Player.transform.position.x) + Mathf.Abs(Pointer.transform.position.z - Player.transform.position.z);
            Distance = (Distance /4);
            Distance = -Distance;
            Distance = Distance - Player.GetComponent<Player>().PlayerWeight;
        }
        UpdateMeshCollider();
        if (Player == null && IsRotating == false)
        {
            LeafBendingReturn();
        }
        else if (Player != null && IsRotating == false)
        {
            LeafBendingByWalking();
        }
        else if (IsRotating == true)
        {
            LeafBendingRotating();
        }
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
    private void RotateBones(GameObject Bone)
    {
        if (Distance < -2)
        {
            float RotationSpeed = 0.5f;
            float TargetRotationAngle = Mathf.Atan2(Distance, 1) * 1.2f * Mathf.Rad2Deg;
            float CurrentRotation = Bone.transform.rotation.eulerAngles.z;
            float NewRotation = Mathf.LerpAngle(CurrentRotation, TargetRotationAngle, RotationSpeed * Time.deltaTime);
            Bone.transform.rotation = Quaternion.Euler(Bone.transform.rotation.eulerAngles.x, Bone.transform.rotation.eulerAngles.y, NewRotation);
        }
        else
        {
            LeafBendingReturn();
        }
    }
    private void RotateBonesReturn(GameObject Bone)
    {
        float RotationSpeed = 0.1f;
        float TargetRotationAngle = Mathf.Atan2(-2.28f, 1) * 1.2f * Mathf.Rad2Deg;
        float CurrentRotation = Bone.transform.rotation.eulerAngles.z;
        float NewRotation = Mathf.LerpAngle(CurrentRotation, TargetRotationAngle, RotationSpeed * Time.deltaTime);
        Bone.transform.rotation = Quaternion.Euler(Bone.transform.rotation.eulerAngles.x, Bone.transform.rotation.eulerAngles.y, NewRotation);
    }
    private void LeafBendingReturn()
    {
        RotateBonesReturn(Bone1);
        RotateBonesReturn(Bone2);
        RotateBonesReturn(Bone3);
        RotateBonesReturn(Bone4);
        RotateBonesReturn(Bone5);
        RotateBonesReturn(Bone6);
    }
    private void LeafBendingRotating()
    {
        RotateBonesRotation(Bone1);
        RotateBonesRotation(Bone2);
        RotateBonesRotation(Bone3);
        RotateBonesRotation(Bone4);
        RotateBonesRotation(Bone5);
        RotateBonesRotation(Bone6);
    }
    private void RotateBonesRotation(GameObject Bone)
    {
        if (TimesOfBending > 0)
        {
            float RotationSpeed = 2f;
            float TargetRotationAngle = Mathf.Atan2(MinValue, 1) * 1.2f * Mathf.Rad2Deg;
            float CurrentRotation = Bone.transform.rotation.eulerAngles.z;
            float NewRotation = Mathf.LerpAngle(CurrentRotation, TargetRotationAngle, RotationSpeed * Time.deltaTime);
            Bone.transform.rotation = Quaternion.Euler(Bone.transform.rotation.eulerAngles.x, Bone.transform.rotation.eulerAngles.y, NewRotation);
            if (!IsNegative)
            {
                MinValue -= Time.deltaTime;
                if (MinValue < (MaxValue - 3f ))
                {
                    MaxValue = MinValue;
                    IsNegative = true;
                    TimesOfBending--;
                }
            }
            else if (IsNegative)
            {
                MinValue += Time.deltaTime;
                if (MinValue > (MaxValue + 0.2f))
                {
                    MaxValue = MinValue;
                    IsNegative = false;
                    TimesOfBending--;
                }
            }
        }
        else
        {
            IsRotating = false;
        }
    }
    public void SetMaxValue()
    {
        Distance = Mathf.Abs(Pointer.transform.position.x - Player.transform.position.x) + Mathf.Abs(Pointer.transform.position.z - Player.transform.position.z);
        Distance = (Distance / 4);
        Distance = -Distance;
        Distance = Distance - Player.GetComponent<Player>().PlayerWeight;
        Landed = Player.GetComponent<Player>().Falling;
        MaxValue = Distance;
        MinValue = MaxValue;
        TimesOfBending = 3;
        IsNegative = true;
    }
}