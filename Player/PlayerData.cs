using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Player Stats
    public float health { get; private set; }
    public float maxHealth { get; private set; }
    public float healSpeed { get; private set; }
    public float reloadSpeedMod { get; private set; }
    public float damageMod { get; private set; }
    public float baseSpeed { get; private set; }
    public float sprintMod { get; private set; }
    public int points { get; private set; }
    public int totalPoints { get; private set; }
    public int pointMod { get; private set; }
    public bool isPowerOn { get; private set; }
    public bool isDead { get; private set; }

    public int GrenadeCount { get; private set; }

    public int FusesCollected { get; private set; }


    // Player Weapon
    [SerializeField] private WeaponData[] weapons = new WeaponData[2];

    [SerializeField] private GameObject[] weaponEquip;
    private int[] perks = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        InitializeStats();
        InitializeWeapons();
    }

    private void InitializeStats()
    {
        isPowerOn = false;

        health = 100;
        maxHealth = health;
        healSpeed = 6f;

        reloadSpeedMod = 1f;
        damageMod = 1f;

        baseSpeed = 4f;
        sprintMod = 0.4f;

        points = 500;
        totalPoints = 500;
        pointMod = 1;

        GrenadeCount = 2;
        FusesCollected = 0;
    }

    private IEnumerator HealOverTime()
    {
        while (!IsFullHealth())
        {
            Debug.Log("Heal");
            yield return new WaitForSeconds(healSpeed);
            if (!IsFullHealth())
            {
                health += 50;
                Debug.Log(health);
            }
        }
    }

    private bool IsFullHealth()
    {
        if (health >= maxHealth || health <= 0)
        {
            return true;
        }
        return false;
    }

    private void InitializeWeapons()
    {
        weapons[0].SetWeaponType(UtilityData.Instance.GetWeaponDatabase().Weapons[1]);
        weapons[1].SetWeaponType(UtilityData.Instance.GetWeaponDatabase().Weapons[0]);
    }

    public void ChangePoints(int c)
    {
        if (c < 0) { points += c; }
        else { points += c * pointMod; }
        if(c > 0)
        {
            totalPoints += c * pointMod;
            UIScoreboardData.Instance.UpdateScore(totalPoints);
        }
    }

    public bool ComparePoints(int target)
    {
        if (points >= target)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeHealth(int c)
    {
        if (isDead) { return; }
        health += c;
        if (!IsFullHealth())
        {
            StartCoroutine(HealOverTime());
        }
        if (health <= 0)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            isDead = true;
        }
    }

    public WeaponData GetEquippedWeapon()
    {
        return weapons[0];
    }

    public WeaponData GetStoredWeapon()
    {
        return weapons[1];
    }

    public void ChangeMaxHealth(float c)
    {
        health += c;
        maxHealth = health;
    }

    public void ChangePointMod(int c)
    {
        pointMod += c;
    }

    public void ChangeDamageMod(float c)
    {
        damageMod += c;
    }

    public void ChangeReloadSpeedMod(float c)
    {
        reloadSpeedMod += c;
    }

    public void ChangeHealSpeed(float c)
    {
        healSpeed += c;
    }

    public void SetPower(bool set)
    {
        isPowerOn = set;
    }

    public void UseGrenade()
    {
        GrenadeCount -= 1;
        if(GrenadeCount < 0)
        {
            GrenadeCount = 0;
        }
        UIData.Instance.UpdateGrenadeCount(GrenadeCount);
    }

    public void RefreshGrenades()
    {
        GrenadeCount = 2;
        UIData.Instance.UpdateGrenadeCount(GrenadeCount);
    }

    public void PickupFuse()
    {
        if(FusesCollected > 2) { return; }
        FusesCollected += 1;
        UIScoreboardData.Instance.UpdateFuses(FusesCollected);
    }

    public void SwapWeapon()
    {
        if(GetStoredWeapon().type.ID != 0)
        {
            weaponEquip[GetEquippedWeapon().type.Hands - 1].SetActive(false);
            WeaponData temp = weapons[0];
            weapons[0] = weapons[1];
            weapons[1] = temp;
            weaponEquip[GetEquippedWeapon().type.Hands - 1].SetActive(true);
        }
    }

    public void AddWeapon(WeaponType type)
    {
        if(GetStoredWeapon().type.ID == 0)
        {
            GetStoredWeapon().SetWeaponType(type);
            weaponEquip[GetEquippedWeapon().type.Hands - 1].SetActive(false);
            SwapWeapon();
            weaponEquip[GetEquippedWeapon().type.Hands - 1].SetActive(true);
        }
        else
        {
            weaponEquip[GetEquippedWeapon().type.Hands - 1].SetActive(false);
            GetEquippedWeapon().SetWeaponType(type);
            weaponEquip[GetEquippedWeapon().type.Hands - 1].SetActive(true);
        }
    }

    public bool CheckWeapon(WeaponType type)
    {
        return type.ID == this.GetEquippedWeapon().type.ID || type.ID == this.GetStoredWeapon().type.ID;
    }

    public int[] GetPerks()
    {
        return perks;
    }

    public int GetPerkAmount()
    {
        int i = 0;
        foreach (int perk in perks)
        {
            if (perk != 0)
            {
                i++;
            }
        }
        return i;
    }

    public bool CheckDuplicatePerks(int id)
    {
        foreach (int perk in perks)
        {
            if (perk == id)
            {
                return true;
            }
        }
        return false;
    }
}
