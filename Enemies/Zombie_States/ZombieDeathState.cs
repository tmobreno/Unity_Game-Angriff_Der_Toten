using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;

public class ZombieDeathState : ZombieBaseState
{
    public override void EnterState(ZombieStateManager state)
    {
        if(state.killerData == null)
        {
            state.SetKiller(FindObjectOfType<PlayerData>());
        }
        state.killerData.ChangePoints(state.type.PointValue);
        ZoneControl.Instance.IncrementKills();
        if (state.fuse != null) { FuseUpdate(state); }
        SpawnPowerUp(state);
        state.animator.SetBool("IsDead", true);
        this.GetComponent<AIPath>().isStopped = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(DespawnBody());
    }

    public override void UpdateState(ZombieStateManager state)
    {
    }

    public void FuseUpdate(ZombieStateManager state)
    {
        if (state.fuse.IsCharged) { return; }
        GameObject f = Instantiate(state.FuseParticle, this.transform.position, this.transform.rotation);
        f.GetComponent<FuseParticle>().SetFuse(state.fuse);
    }

    private IEnumerator DespawnBody()
    {
        yield return new WaitForSeconds(5f);
        Vector3 t = this.transform.position;
        this.transform.DOLocalMove(new Vector3(t.x, t.y, t.z + 10), 4f).SetEase(Ease.InOutFlash).OnComplete(() => Destroy(this.gameObject));
    }

    private void SpawnPowerUp(ZombieStateManager state)
    {
        int rand = Random.Range(0, 100);
        if (rand > 96 && state.isInZone)
        {
            Instantiate(state.powerUp, this.transform.position, this.transform.rotation);
        }
    }
}
