using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunReload : MonoBehaviour
{
    GunManager gunManager;

    private void OnEnable()
    {
        PlayerReloading.OnReload += ReloadAmmo;
    }
    private void OnDisable()
    {
        PlayerReloading.OnReload -= ReloadAmmo;
    }

    private void Awake()
    {
        gunManager = GetComponent<GunManager>();
    }

    public void ReloadAmmo()
    {
        if(gunManager.currentAmmoHolding > 0)
        {
            int amountAmmoToReLoad = gunManager.ammoCapacity - gunManager.currentAmmo;
            if (amountAmmoToReLoad >= gunManager.currentAmmoHolding)
            {
                amountAmmoToReLoad = gunManager.currentAmmoHolding;
            }
            gunManager.currentAmmo += amountAmmoToReLoad;
            gunManager.currentAmmoHolding -= amountAmmoToReLoad;
        } 
    }
}
