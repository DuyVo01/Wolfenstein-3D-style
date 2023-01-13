using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoBoxInteracting : MonoBehaviour, IInteractable
{
    GunManager gunManager;
    public int ammoAmount;
    public AudioClip ammoAudio;
    public TextMeshProUGUI ammoNotice;

    private void Awake()
    {
        
    }
    private void Update()
    {
        if (PlayerStatus.isWeaponEquipped)
        {
            if(gunManager != PlayerStatus.currentWeaponEquipped)
            {
                gunManager = PlayerStatus.currentWeaponEquipped.GetComponent<GunManager>();
            }
        }
    }
    public void OnInteract()
    {
        if(gunManager != null && gunManager.CompareTag("Gun"))
        {
            if(gunManager.currentAmmoHolding != gunManager.ammoHoldingCapacity)
            {
                AmmoPickup();
            }
        }
    }

    private void AmmoPickup()
    {
        int amount = ammoAmount + gunManager.currentAmmoHolding;
        if(amount > gunManager.ammoHoldingCapacity)
        {
            amount = gunManager.ammoHoldingCapacity;
        }
        gunManager.currentAmmoHolding = amount;
        EnvironmentObjects.PlayObjectAudio(ammoAudio);
        gameObject.SetActive(false);
        DisplayNotice.AddText("Ammo + " + ammoAmount);
    }

}
