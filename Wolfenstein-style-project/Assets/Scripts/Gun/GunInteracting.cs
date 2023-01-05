using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunInteracting : MonoBehaviour, IInteractable
{
    public Transform weaponPos;
    MultiParentConstraint constraint;
    private void Awake()
    {
        constraint = weaponPos.GetComponent<MultiParentConstraint>();
    }
    public void OnInteract()
    {
        OnPickUp();
    }

    public void OnPickUp()
    {
        transform.SetParent(weaponPos);
        constraint.data.constrainedObject = transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
