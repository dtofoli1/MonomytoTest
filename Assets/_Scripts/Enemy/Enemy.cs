using System;
using UnityEngine;

public class Enemy : Player
{
    public static event Action<Enemy> OnEnemyKilled;
    public override void OnEnable()
    {
        EquipWeapon(defaultWeapon);
        this.maxHP = 5;
        currentHP = maxHP;
    }

    public override void Disable()
    {

    }

    public override void Interaction(int value = 0)
    {
        if (value > 0)
        {
            TakeDamage(value);
        }
    }

    public override void CheckHealth()
    {
        if (currentHP < 1)
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }
}
