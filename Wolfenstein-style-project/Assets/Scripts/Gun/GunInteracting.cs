using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunInteracting : MonoBehaviour, IInteractable
{
    public Transform weaponPos;
    public Transform handIK;
    public AudioClip pickupSound;

    public RigBuilder rigBuilder;
    MultiParentConstraint constraint;
    Rig handIKRig;
    
    public bool isEquip = false;
    private void Awake()
    {
        constraint = weaponPos.GetComponent<MultiParentConstraint>();
        handIKRig = handIK.GetComponent<Rig>();
    }
    public void OnInteract()
    {
        if (!isEquip)
        {
            OnPickUp();
        } 
    }

    private void Update()
    {
        if (!isEquip)
        {
            handIK.gameObject.SetActive(false);
            handIKRig.weight = 0;
        }
    }

    public void OnPickUp()
    {
        Setup();
        GunAudioManager.PlayAudio(pickupSound);
        DisplayNotice.AddText("HandGun");
    }

    public void Setup()
    {
        transform.SetParent(weaponPos);
        constraint.data.constrainedObject = transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
        handIKRig.weight = 1;
        isEquip = true;
        rigBuilder.Build();
    }
}
