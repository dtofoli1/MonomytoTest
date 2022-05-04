using System;
using UnityEngine;

public class DestructableBox : InteractableObject
{
    public static event Action<DestructableBox> OnBoxDestruct;
    private float currentHP;
    private float maxHP = 3;

    public override void OnEnable()
    {
        base.OnEnable();
        currentHP = maxHP;
    }

    public override void Interaction(int value = 0)
    {
        if (value > 0)
        {
            TakeDamage(value);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP = currentHP - damage;
        CheckHealth();
    }

    public void CheckHealth()
    {
        if (currentHP < 1)
        {
            OnBoxDestruct?.Invoke(this);
            Disable();
        }
    }

    public override void Disable()
    {
        rigidbody.gameObject.SetActive(false);
    }
}
