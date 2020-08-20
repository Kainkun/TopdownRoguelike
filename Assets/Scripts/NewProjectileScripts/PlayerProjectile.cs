using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : PlayerBullet
{
    #region Stats variables
    public float collisionRadius = 0.2f;
    public Vector2 startMovementDirection;
    public float startSpeed;
    #endregion

    #region Management variables
    Vector2 lastPosition;
    LineRenderer lineRenderer;

    #endregion


    protected override void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();

        base.Awake();
    }

    void Start()
    {
        lineRenderer.SetPosition(0, transform.position);
        lastPosition = transform.position;

    }


    void Update()
    {
        if (dead)
            return;


        Movement();

        lineRenderer.SetPosition(1, transform.position);

        Debug.DrawRay(lastPosition, (Vector2)transform.position - lastPosition, Color.red);
    }
    void Movement()
    {
        transform.position += (Vector3)startMovementDirection * startSpeed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.CircleCast(lastPosition, collisionRadius, (Vector2)transform.position - lastPosition, ((Vector2)transform.position - lastPosition).magnitude, validColliders);
        if (hit.collider != null)
        {
            Collision(hit);
        }
    }

    private void LateUpdate()
    {
        lastPosition = transform.position;
    }

    protected virtual void Collision(RaycastHit2D hit)
    {
        hit.collider.GetComponent<Entity>()?.TakeDamage(damage);
        Die(hit.centroid);
    }

    public void Die(Vector2 positionOfDeath)
    {
        
        lineRenderer.SetPosition(1, positionOfDeath);
        lineRenderer.transform.parent = null;

        if (collisionParticles != null)
            Destroy(Instantiate(collisionParticles, positionOfDeath, Quaternion.identity), 2);

        base.Die();
    }
}
