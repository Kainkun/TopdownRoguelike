using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Entity>()?.TakeDamage(damage);
    }

    public void TurnOnTrigger()
    {
        col.enabled = true;
    }

    public void TurnOffTrigger()
    {
        col.enabled = false;
    }
}
