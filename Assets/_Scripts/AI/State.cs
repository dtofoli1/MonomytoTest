using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void EnterState(EnemyBehaviour behaviour);

    public abstract void UpdateState(EnemyBehaviour behaviour);
}
