using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GunManager guns;

    private GunInteracting setupGun;
    private void Start()
    {
        LoadSavedSettings();
        if (!SceneManager.GetActiveScene().name.Equals("Level01"))
        {
            LoadSavedData();
            Debug.Log("LOAD");
        }
    }

    private void LoadSavedData()
    {
        setupGun = guns.GetComponent<GunInteracting>();
        if (PlayerPrefs.GetString("CurrentGun").Equals(guns.gameObject.tag))
        {
            playerInventory.AddItem(guns.gameObject);
            setupGun.Setup();
            guns.currentAmmoHolding = PlayerPrefs.GetInt("CurrentGunAmmo");
        }
        Debug.Log(LevelRecord.Instance.IsEndGame);
        
    }

    private void LoadSavedSettings()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float localVolume = PlayerPrefs.GetFloat("MasterVolume");
            AudioListener.volume = localVolume;
        }

        if (PlayerPrefs.HasKey("MasterQuality"))
        {
            int localQuality = PlayerPrefs.GetInt("MasterQuality");
            QualitySettings.SetQualityLevel(localQuality);
        }

        if (PlayerPrefs.HasKey("MasterFullScreen"))
        {
            int localFullscreen = PlayerPrefs.GetInt("MasterFullScreen");

            if (localFullscreen == 1)
            {
                Screen.fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
            }
        }

        if (PlayerPrefs.HasKey("MasterBrightness"))
        {
            float localBrightness = PlayerPrefs.GetFloat("MasterBrightness");
        }
    }
}
