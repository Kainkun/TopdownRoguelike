using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    #region Bullet stats variables
    public float damage;
    public float maxDistance;
    public SpriteRenderer sprite;
    public GameObject collisionParticles;
    #endregion

    #region Management variables
    protected Vector2 startPosition;
    protected Vector2 velocity;
    protected LayerMask validColliders;
    protected bool dead;
    #endregion

    protected virtual void Awake()
    {
        startPosition = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
        validColliders = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("PlayerProjectile"));
    }

    protected virtual void Die()
    {
        print("WTF");
        dead = true;
        if (sprite != null)
            sprite.enabled = false;
    }

}
