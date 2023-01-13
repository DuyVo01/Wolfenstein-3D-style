using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    private GameObject currentGun;
    private GunManager gunManager;

    private void Start()
    {
        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
        Save();
    }

    public void Save()
    {
        currentGun = PlayerStatus.currentWeaponEquipped;
        if(currentGun != null)
        {
            gunManager = currentGun.GetComponent<GunManager>();
            PlayerPrefs.SetString("CurrentGun", currentGun.tag);
            PlayerPrefs.SetInt("CurrentGunAmmo", gunManager.currentAmmoHolding);
        }
        else
        {
            PlayerPrefs.SetString("CurrentGun", "");
            PlayerPrefs.SetInt("CurrentGunAmmo", 0);
        }
    }

   
}
