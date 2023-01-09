using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponAimingRotate : MonoBehaviour
{
    public MultiAimConstraint aiming;
    private void Awake()
    {
        aiming.weight = 0;
    }
    public void Aiming()
    {
        aiming.weight = 1;
    }

    public void ResetAiming()
    {
        aiming.weight = 0;
    }
}
