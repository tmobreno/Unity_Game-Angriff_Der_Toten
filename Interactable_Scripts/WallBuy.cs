using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuy : Interactable
{
    [SerializeField]
    private WeaponType type;

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        cost = type.WallCost;
        notificationText = "Hold 'F' to buy " + type.Name + " (Cost: " + type.WallCost + ")";
    }

    public override void Interact()
    {
        if (IsDuplicate()) { return; }
        PurchaseWallBuy(type);
        SubCost();
    }

    private void PurchaseWallBuy(WeaponType type)
    {
        data.AddWeapon(type);
    }

    private bool IsDuplicate()
    {
        if (data.CheckWeapon(type))
        {
            return true;
        }
        return false;
    }
}
