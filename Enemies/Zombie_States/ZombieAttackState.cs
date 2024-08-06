using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieAttackState : ZombieBaseState
{
    public override void EnterState(ZombieStateManager state)
    {
        state.animator.SetBool("Attack", true);
        this.GetComponent<AIPath>().isStopped = true;
        StartCoroutine(AttackTarget(state));
    }

    public override void UpdateState(ZombieStateManager state)
    {
        if (!this.GetComponent<AIPath>().reachedEndOfPath)
        {
            this.GetComponent<AIPath>().isStopped = true;
            state.animator.SetBool("Attack", false);
            state.SwitchState(state.TargetState);
        }
    }

    private IEnumerator AttackTarget(ZombieStateManager state)
    {
        while (this.GetComponent<AIPath>().reachedEndOfPath)
        {
            yield return new WaitForSeconds(state.type.AttackSpeed);
            if (state.health <= 0) { break; }
            if (this.GetComponent<AIPath>().reachedEndOfPath)
            {
                if (state.data != null && (state.data.gameObject.transform.position - this.gameObject.transform.position).magnitude < 2)
                {
                    state.data.ChangeHealth(-50);
                    Instantiate(state.bloodParticles, state.data.transform.position, state.data.transform.rotation);
                    Debug.Log(FindObjectOfType<PlayerData>().health);
                }
                else
                {
                    state.spawner.Window.DestroyBoard(state.spawner.Window.GetWindowHealth());
                    state.spawner.Window.ChangeWindowHealth(-1);
                    if(state.spawner.Window.GetWindowHealth() == 0)
                    {
                        this.GetComponent<AIPath>().isStopped = false;
                        state.animator.SetBool("Attack", false);
                        state.SwitchState(state.TargetState);
                    }
                }
            }
            else
            {
                this.GetComponent<AIPath>().isStopped = false;
                state.animator.SetBool("Attack", false);
                state.SwitchState(state.TargetState);
            }
        }
    }
}
