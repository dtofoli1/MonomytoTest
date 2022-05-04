using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Pistol")]
public class Pistol : Weapon
{
    public override void Shoot(Transform firePoint)
    {
        GameObject projectile = ObjectPooler.instance.GetPooledObject();
        if (projectile == null) return;
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        projectile.SetActive(true);
        //projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile.GetComponent<Rigidbody>().AddForce(firePoint.forward * travelForce, ForceMode.Impulse);
    }
}
