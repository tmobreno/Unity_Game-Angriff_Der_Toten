using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FuseParticle : MonoBehaviour
{
    private Fuse _fuse;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 t = this.transform.position;
        this.transform.DOLocalMove(_fuse.transform.position, 3f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            _fuse.IncrementCharge();
            Destroy(this.gameObject);
        });
    }

    public void SetFuse(Fuse fuse)
    {
        _fuse = fuse;
    }
}
