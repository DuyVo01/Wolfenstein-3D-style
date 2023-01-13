using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour, IDamagable
{
    public static GameObject currentWeaponEquipped;
    public static bool isWeaponEquipped;

    public AudioSource audioSource;
    public AudioClip[] hurt;
    public Rig bodyAimRig;
    public Rig bodyIdleRig;

    [Header("Status")]
    public float health;
    public int lives;
    public static float currentHealth;
    public static int currentLives;
    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        currentLives = PlayerPrefs.GetInt("CurrentLives");
        isWeaponEquipped = false;
    }

    public void Damage(float damageAmount)
    {
        TakingDamage(damageAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelRecord.Instance.IsEndGame)
        {
            currentWeaponEquipped = null;
            isWeaponEquipped = false;
        }
        if (currentWeaponEquipped != null)
        {
            isWeaponEquipped = true;
            bodyAimRig.weight = 1;
            bodyIdleRig.weight = 0;
        }
        else
        {
            isWeaponEquipped = false;
            bodyAimRig.weight = 0;
            bodyIdleRig.weight = 1;
        }
    }

    private void TakingDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        DisplayNotice.HitNotice();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            currentLives--;
            Death();
        }
        if (!audioSource.isPlaying)
        {
            int index = Random.Range(0, hurt.Length - 1);
            audioSource.clip = hurt[index];
            audioSource.PlayDelayed(0.2f);  
        }
    }

    private void Death()
    {
        isWeaponEquipped = false;
        currentWeaponEquipped = null;
        
        SceneManager.LoadScene("LevelRecycleScene");
    }

    public static void AddScore(int scoretoAdd)
    {
        score += scoretoAdd;
        LevelRecord.Instance.UpdateScore(score);
    }

    public static void Recover(int amountOfHealth)
    {
        currentHealth += amountOfHealth;
        if(currentHealth > 100)
        {
            currentHealth = 100;
        }
    }
}
