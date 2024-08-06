using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : Interactable
{
    public bool IsCharged { get; private set; }

    [field : SerializeField] public int ChargeCollected { get; private set; }
    [field: SerializeField] public int MaxCharge { get; private set; }

    [SerializeField] private GameObject aura;

    [SerializeField] private GameObject empty;

    [SerializeField] private GameObject full;

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        notificationText = "Hold 'F' to pickup Fuse";
    }

    public override void Interact()
    {
        if (!IsCharged) { return; }
        data.PickupFuse();
        Destroy(this.gameObject);
    }

    public override void SetNotificationText()
    {
        if (!IsCharged) { return; }
        base.SetNotificationText();
    }

    public void IncrementCharge()
    {
        if (ChargeCollected > MaxCharge) { return; }
        ChargeCollected += 1;
        if (ChargeCollected == MaxCharge) {
            empty.SetActive(false);
            full.SetActive(true);
            IsCharged = true;
            Destroy(aura.gameObject);
        }
    }

}
