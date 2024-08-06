using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grenade : MonoBehaviour
{
    public Transform EndPosition { get; set; }
    private float damageRadius = 4f;
    private int damageAmount = 200;

    [SerializeField]
    private GameObject explosionFX;

    private void Start()
    {
        this.transform.DOLocalMove(EndPosition.position, 1).OnComplete(()=>
        {
            StartCoroutine(Countdown());
        });
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(2f);
        Explode();
    }

    private void Explode()
    {
        Instantiate(explosionFX, this.transform.position, this.transform.rotation);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<ZombieStateManager>();
            if(enemy != null)
            {
                enemy.ChangeHealth(-damageAmount);
            }
        }
        Destroy(this.gameObject);
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
    */
}
