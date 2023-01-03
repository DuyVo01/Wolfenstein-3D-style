using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimingRotate : MonoBehaviour
{
    Detection_Test detection;
    Animator animator;
    Vector3 targetPlayer;
    public Transform aimPosition;
    public float currentRotateAngle { get; private set; }

    public EnemyBones[] bones;

    [Range(0, 1)]
    public float weight = 1;
    private void Awake()
    {
        detection = GetComponent<Detection_Test>();
        animator = GetComponent<Animator>();
    }

    public void Aiming()
    {
        targetPlayer = detection.targetPlayerPosition;
        for (int i = 0; i < bones.Length; i++)
        {
            Transform bone = animator.GetBoneTransform(bones[i].bone);
            float boneWeight = bones[i].boneWeight * weight;
            AimAtTarget(bone, targetPlayer, boneWeight);
        }
        currentRotateAngle = Vector3.Angle(aimPosition.position, targetPlayer);
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition, float boneWeight)
    {
        Vector3 targetDirection = targetPosition - aimPosition.position;
        Quaternion aimTowards = Quaternion.LookRotation(targetDirection);
        aimTowards *= Quaternion.Euler(new Vector3(0, 40, 0));
        bone.rotation = Quaternion.Slerp(bone.rotation, aimTowards, boneWeight * Time.deltaTime);
    }
}
