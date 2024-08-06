using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [field: SerializeField] public WeaponType type { get; private set; }
    public float firingTime { get; private set; }
    public float damage { get; private set; }
    public float reloadSpeed { get; private set; }
    public int remainingAmmo { get; private set; }
    public int clipAmmo { get; private set; }
    public int storedAmmo { get; private set; }

    public GameObject bullet { get; private set; }

    public void SetWeaponType(WeaponType _type)
    {
        type = _type;
        firingTime = _type.FiringTime;
        damage = _type.Damage;
        bullet = _type.Bullet;
        storedAmmo = _type.StoredAmmo;
        clipAmmo = _type.ClipAmmo;
        remainingAmmo = clipAmmo;
        reloadSpeed = _type.ReloadSpeed;
    }

    public void RefillWeapon()
    {
        storedAmmo = type.StoredAmmo;
        clipAmmo = type.ClipAmmo;
        remainingAmmo = clipAmmo;
    }

    public void ChangeRemainingAmmo(int c)
    {
        if(remainingAmmo > 0)
        {
            remainingAmmo += c;
        }
    }

    public IEnumerator ReloadWeapon(float reloadSpeedMod)
    {
        int tempAmmo = clipAmmo - remainingAmmo;
        yield return new WaitForSeconds(this.reloadSpeed / reloadSpeedMod);
        if (storedAmmo > tempAmmo)
        {
            remainingAmmo = clipAmmo;
            storedAmmo -= tempAmmo;
        }
        else
        {
            remainingAmmo += storedAmmo;
            storedAmmo = 0;
        }
    }

    public bool CheckRemainingAmmo()
    {
        return (remainingAmmo > 0);
    }
}
