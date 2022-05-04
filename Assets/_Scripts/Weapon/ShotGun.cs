using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Shotgun")]
public class ShotGun : Weapon
{
    public List<GameObject> bullets;
    public float spreadAngle;
    public float lifeTime;
    List<Quaternion> pellets = new List<Quaternion>();
    public override void Shoot(Transform firePoint)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject tempProjectile = ObjectPooler.instance.GetPooledObject();
            if (tempProjectile == null) return;
            bullets.Add(tempProjectile);
            tempProjectile.SetActive(true);
        }

        HandleBullet(firePoint, bullets[0], 0);
        HandleBullet(firePoint, bullets[1], spreadAngle);
        HandleBullet(firePoint, bullets[2], spreadAngle * -1);

        bullets.Clear();
    }

    void HandleBullet(Transform firePoint, GameObject bullet, float usedSpreadAngle)
    {
        //bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation * Quaternion.AngleAxis(usedSpreadAngle, Vector3.up);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * travelForce, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().Invoke("Disable", lifeTime);
    }
}
