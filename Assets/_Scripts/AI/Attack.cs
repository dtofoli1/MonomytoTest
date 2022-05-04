using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Attack")]
public class Attack : State
{
    public override void EnterState(EnemyBehaviour behaviour)
    {
        Debug.Log("FOUND PLAYER, ATTACKING");
    }

    public override void UpdateState(EnemyBehaviour behaviour)
    {
        behaviour.transform.LookAt(behaviour.target);
        behaviour.enemy.Shoot();
    }
}
