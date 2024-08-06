using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class ZombieBaseState : MonoBehaviour
{
    public abstract void EnterState(ZombieStateManager state);

    public abstract void UpdateState(ZombieStateManager state);
}
