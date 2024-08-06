using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieTargetState : ZombieBaseState
{
    public override void EnterState(ZombieStateManager state)
    {
        state.animator.SetBool("Attack", false);
    }

    public override void UpdateState(ZombieStateManager state)
    {
        // Target Player
        if (state.isInZone)
        {
            this.GetComponent<AIDestinationSetter>().target = FindNearest("Player").transform;
            state.SetPlayer(this.GetComponent<AIDestinationSetter>().target.GetComponent<PlayerData>());
        }
        // Target Zone
        else if (state.spawner.HasWindow() && state.spawner.Window.GetWindowHealth() == 0 && !state.isInZone)
        {
            this.GetComponent<AIDestinationSetter>().target = FindNearest("Zone").transform;
        }
        // Target Window
        else if (state.spawner.HasWindow() && state.spawner.Window.GetWindowHealth() >= 0 && !state.isInZone)
        {
            this.GetComponent<AIDestinationSetter>().target = state.spawner.Window.GetTarget().gameObject.transform;
        }

        // Attack Switch
        if (this.GetComponent<AIPath>().reachedEndOfPath)
        {
            state.SwitchState(state.AttackState);
        }
    }

    private GameObject FindNearest(string tag)
    {
        var arr = GameObject.FindGameObjectsWithTag(tag);
        float dist = float.PositiveInfinity;
        GameObject nearest = null;
        foreach (var go in arr)
        {
            var d = (go.transform.position - this.transform.position).sqrMagnitude;
            if (d < dist)
            {
                nearest = go;
                dist = d;
            }
        }
        return nearest;
    }
}
