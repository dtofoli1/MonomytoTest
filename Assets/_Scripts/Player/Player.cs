using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : InteractableObject
{
    public float maxHP = 10;
    public float currentHP;
    public Image healthBar;

    public Weapon currentWeapon;
    public Weapon defaultWeapon;
    public int currentAmmo;
    public Transform firePoint;
    public bool recoil;

    public override void OnEnable()
    {
        EquipWeapon(defaultWeapon);
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

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.fillAmount = currentHP / maxHP;
        CheckHealth();
    }

    public virtual void CheckHealth()
    {
        if (currentHP < 1)
        {
            GameManager.instance.GameOver();
        }
    }

    public void Shoot()
    {
        if (canShoot())
        {
            currentWeapon.Shoot(firePoint);
            currentAmmo--;
            if (gameObject.tag == "Player") GameManager.instance.uiManager.UpdateText(GameManager.instance.uiManager.currentAmmo, currentAmmo.ToString());
            StartCoroutine(HandleRecoil());
        }
    }

    public void EquipWeapon(Weapon weaponToEquip)
    {
        currentWeapon = weaponToEquip;
        currentAmmo = weaponToEquip.ammo;
        if (gameObject.tag == "Player")
        {
            GameManager.instance.uiManager.UpdateText(GameManager.instance.uiManager.equippedWeapon, currentWeapon.name);
            GameManager.instance.uiManager.UpdateText(GameManager.instance.uiManager.currentAmmo, currentAmmo.ToString());
        }
    }

    public bool canShoot()
    {
        if (recoil)
        {
            return false;
        }

        if (currentAmmo < 1)
        {
            EquipWeapon(defaultWeapon);
            return false;
        }

        return true;
    }

    public IEnumerator HandleRecoil()
    {
        recoil = true;
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while (percent < currentWeapon.recoilTime)
        {
            percent += Time.deltaTime;
            yield return update;
        }
        recoil = false;
    }
}
