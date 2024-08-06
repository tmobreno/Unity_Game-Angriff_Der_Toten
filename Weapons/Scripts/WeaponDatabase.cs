using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponDatabase : ScriptableObject
{
    public WeaponType[] Weapons = new WeaponType[10];
}
