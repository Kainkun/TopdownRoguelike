using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Entity
{
    [SerializeField] Transform targetDestination;
    protected AIPath aiPath;

    protected override void Awake()
    {
        aiPath = GetComponent<AIPath>();
        targetDestination.parent = null;

        base.Awake();
    }

    protected virtual void Start()
    {
        StartCoroutine(EnemyLoop());
    }

    protected virtual IEnumerator EnemyLoop() {yield return null;}

    protected virtual void MoveTo(Vector2 position)
    {
        targetDestination.position = position;
    }
}
