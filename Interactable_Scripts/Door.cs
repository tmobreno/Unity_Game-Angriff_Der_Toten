using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : Interactable
{
    public enum Type
    {
        Up,
        Side
    }

    [SerializeField]
    private Type doorType;

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        notificationText = "Hold 'F' to buy Door (Cost: " + cost + ")";
    }

    public override void Interact()
    {
        SubCost();
        ClearNotificationText();
        Vector3 t = this.transform.position;
        if(doorType == Type.Up)
        {
            this.transform.DOLocalMove(new Vector3(t.x, t.y, t.z - 20), .8f).SetEase(Ease.InOutFlash).OnComplete(() => Destroy(this.gameObject));
        }
        if (doorType == Type.Side)
        {
            this.transform.DOLocalMove(new Vector3(t.x + 3, t.y, t.z), .8f).SetEase(Ease.InOutFlash).OnComplete(() => Destroy(this.gameObject));
        }
    }
}
