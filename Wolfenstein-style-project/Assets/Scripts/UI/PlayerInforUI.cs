using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.UI;

public class PlayerInforUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI holdingAmmo;
    public TextMeshProUGUI currentHealth;
    public TextMeshProUGUI currentLife;
    public TextMeshProUGUI score;
    public TextMeshProUGUI keyAmount;
    public Image gunImage;
    public List<ImagehealthData> healthImages;
    GunManager gunInfor;

    private void Awake()
    {
        //healthImages = new List<ImagehealthData>();   
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerStatus.isWeaponEquipped)
        {
            gunInfor = PlayerStatus.currentWeaponEquipped.GetComponent<GunManager>();
            currentAmmo.text =  gunInfor.currentAmmo + "";
            currentAmmo.fontSize = 80;
            holdingAmmo.text = "/" + gunInfor.currentAmmoHolding;
            gunImage.sprite = gunInfor.gunImage;
            gunImage.gameObject.SetActive(true);
        }
        else
        {
            EmptyField();
        }

        currentHealth.text = PlayerStatus.currentHealth + "";
        currentLife.text = "" + PlayerStatus.currentLives;
        score.text = "" + PlayerStatus.score;
        keyAmount.text = "x " + playerInventory.GetItemCount("Key");
        ChooseHealthImages(PlayerStatus.currentHealth);
    }

    private void EmptyField()
    {
        currentAmmo.text = "";
        holdingAmmo.text = "";
        gunImage.gameObject.SetActive(false);
        gunImage.sprite = null;
    }

    private void ChooseHealthImages(float currentHealth)
    {
        foreach(ImagehealthData item in healthImages)
        {
            if(item.relativeHealth >= currentHealth)
            {
                item.healthImages.SetActive(true);
            }
            else
            {
                item.healthImages.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public class ImagehealthData
{
    [field: SerializeField]
    public GameObject healthImages { get; set; }
    [field: SerializeField]
    public float relativeHealth { get; set; }
}
