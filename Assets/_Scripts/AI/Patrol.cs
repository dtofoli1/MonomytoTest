using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Patrol")]
public class Patrol : State
{
    public float speed;
    public float waitTime;

    public override void EnterState(EnemyBehaviour behaviour)
    {
        Debug.Log("AI STATE IS PATROL");
    }

    public override void UpdateState(EnemyBehaviour behaviour)
    {
        if (behaviour.transform.position != behaviour.patrolPoints[behaviour.currentPatrolPoint].position)
        {
            behaviour.transform.position = Vector3.MoveTowards(behaviour.transform.position, behaviour.patrolPoints[behaviour.currentPatrolPoint].position, speed * Time.deltaTime);
            behaviour.transform.LookAt(behaviour.patrolPoints[behaviour.currentPatrolPoint]);
        }
        else
        {
            if (!behaviour.waiting)
            {
                behaviour.waiting = true;
                behaviour.StartCoroutine(behaviour.Wait(waitTime));
            }
        }
    }
}
