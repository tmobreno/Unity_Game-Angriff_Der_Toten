using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponType : ScriptableObject
{
    public string Name, Description;
    public int ID;
    public GameObject Bullet;
    public float FiringTime, Damage, ReloadSpeed, Scatter;
    public int Pellets, ClipAmmo, StoredAmmo, WallCost, Hands;
    public bool Burst;

    /* ID Values
     * 
     * 0 - No Weapon
     * 1 - M1911
     * 2 - AK-47
     * 3 - Remington
     * 4 - MP5
     * 5 - M14
     * 6 - Galil
     * 7 - RK5
     * 8 - FAL
     * 9 - Type 55
     * 10 - M1216
     * 11 - AN-94
     * 12 - Five Seven
     * 13 - RPK
     * 14 - DSR-100
     * 15 - SVG-B
     * 16 - Executioner 
     * 17 - Wind Gun
     * 18 - Ray Gun
     */
}
