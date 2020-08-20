using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] float maxHealth;
    float currentHealth = 1;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        if(currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}