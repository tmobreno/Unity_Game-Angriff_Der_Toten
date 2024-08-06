using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyType : ScriptableObject
{
    public string Name, Description;
    public int ID, Health;
    public float Speed, AttackSpeed;
    public int PointValue;

    /* ID Values
     * 
     * 1 - Basic_Zombie
     * 2 - Dog_Zombie
     * 3 -
     * 
     */
}
