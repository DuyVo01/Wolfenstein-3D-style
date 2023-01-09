using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class PlayerInforUI : MonoBehaviour
{
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI holdingAmmo;
    public TextMeshProUGUI currentHealth;
    public TextMeshProUGUI currentLife;
    public TextMeshProUGUI score;
    GunManager gunInfor;

    private void Awake()
    {
        
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
        }
        else
        {
            EmptyText();
        }

        currentHealth.text = PlayerStatus.currentHealth + "";
        currentLife.text = "" + PlayerStatus.currentLives;
        score.text = "" + PlayerStatus.score;
    }

    private void EmptyText()
    {
        currentAmmo.text = "";
        holdingAmmo.text = "";
    }
}
