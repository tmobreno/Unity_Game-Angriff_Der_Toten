using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityData : MonoBehaviour
{
    [SerializeField]
    private WeaponDatabase weaponDatabase;
    [SerializeField]
    private PowerUpDatabase powerUpDatabase;

    public static UtilityData Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public WeaponDatabase GetWeaponDatabase()
    {
        return weaponDatabase;
    }

    public PowerUpDatabase GetPowerUpDatabase()
    {
        return powerUpDatabase;
    }
}
