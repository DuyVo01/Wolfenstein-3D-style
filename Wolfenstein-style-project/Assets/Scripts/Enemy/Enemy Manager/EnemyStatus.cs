using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour, IDamagable
{
    public float enemyHealth;
    public float enemyCurrentHealth;

    public bool isTakingDamage;

    private void Start()
    {
        enemyCurrentHealth = enemyHealth;
    }

    public void Damage(float damageAmount)
    {
        enemyCurrentHealth -= damageAmount;
        isTakingDamage = true;
    }

    
}
