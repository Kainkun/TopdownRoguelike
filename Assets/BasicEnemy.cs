using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    float timeStandingStill;
    [SerializeField] Vector2 waitTimeRange = new Vector2(1f, 3f);
    float nextWaitTime;

    protected override void Start()
    {
        nextWaitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
        timeStandingStill = Random.Range(waitTimeRange.x, nextWaitTime);

        base.Start();
    }

    private void Update()
    {
        if (aiPath.velocity.magnitude <= 0)
        {
            timeStandingStill += Time.deltaTime;
        }
        else
        {
            timeStandingStill = 0;
        }
    }

    protected override IEnumerator EnemyLoop()
    {
        while (true)
        {
            if (timeStandingStill > nextWaitTime)
            {
                timeStandingStill = 0;
                MoveTo((Vector2)transform.position + Random.insideUnitCircle * 5);
                nextWaitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
            }






            yield return null;
        }
    }
}
