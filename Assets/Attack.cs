using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector2 movement;
    public float damage = 1;
    public float duration = 1;
    float time;
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        transform.position += (Vector3)movement * Time.deltaTime;
        time += Time.deltaTime;
        if(time > 0.2f)
        {
            col.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Entity>()?.TakeDamage(damage);
    }
}
