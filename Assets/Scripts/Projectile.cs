using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Stats Variables
    public float damage = 1;
    public float maxDistance;
    [SerializeField] float collisionRadius = 0.2f;
    #endregion

    #region Properties Variables
    SpriteRenderer sprite;
    public Vector2 movementVector;
    [SerializeField] float timeToFadeOut = 2;
    #endregion

    #region Management Variables
    Vector2 startPosition;
    Vector2 lastPosition;
    LayerMask validColliders;
    LineRenderer lineRenderer;
    [SerializeField] GameObject ps_sparks;
    bool dead;
    #endregion

    private void Start()
    {
        startPosition = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lastPosition = transform.position;
        validColliders = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("PlayerProjectile"));
    }
    void Update()
    {
        if (dead)
            return;

        transform.position += (Vector3)movementVector * Time.deltaTime;
        lineRenderer.SetPosition(1, transform.position);

        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(lastPosition, collisionRadius, position - lastPosition, (position - lastPosition).magnitude, validColliders);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Entity>()?.TakeDamage(damage);
            Die(hit.centroid);
        }

        if (maxDistance != 0 && Vector2.Distance(startPosition, position) > maxDistance)
        {
            Die(startPosition + movementVector.normalized * maxDistance);
        }

        Debug.DrawRay(lastPosition, position - lastPosition, Color.red);
    }
    private void LateUpdate()
    {
        lastPosition = transform.position;
    }
    void Die(Vector2 positionOfDeath)
    {
        dead = true;
        lineRenderer.SetPosition(1, positionOfDeath);
        lineRenderer.transform.parent = null;
        StartCoroutine(CR_Fade());
        if (sprite != null)
            sprite.enabled = false;
        Destroy(Instantiate(ps_sparks, positionOfDeath, Quaternion.identity), 2);
    }
    IEnumerator CR_Fade()
    {
        while (lineRenderer.endColor.a > 0)
        {
            var color = lineRenderer.endColor;
            color.a -= Time.deltaTime / timeToFadeOut;
            lineRenderer.endColor = color;

            yield return null;
        }

        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }
}
