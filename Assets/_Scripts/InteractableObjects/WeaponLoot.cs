using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoot : InteractableObject
{
    public Weapon weapon;

    public override void Interaction(int value = 0)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Disable();
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.EquipWeapon(weapon);
        }        
    }

    public override void Disable()
    {
        Destroy(gameObject);
    }
}
