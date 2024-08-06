using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMachine : Interactable
{
    [SerializeField]
    private int type;

    [SerializeField]
    GameObject panel;

    private string[] nameMapping = new string[4] {"Quick Revive", "Juggernog", "Speed Cola", "Double Tap"};

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        notificationText = "Hold 'F' to buy " + nameMapping[type - 1] + " (Cost: " + cost + ")";
    }

    public override void Interact()
    {
        if (data.GetPerkAmount() >= 4 || IsDuplicate() || !data.isPowerOn) { return; }
        data.GetPerks()[data.GetPerkAmount()] = type;
        PerkAction();
        ClearNotificationText();
        SubCost();
    }

    public override void SetNotificationText()
    {
        if (IsDuplicate() || !data.isPowerOn) { return; }
        base.SetNotificationText();
    }

    private void PerkAction()
    {
        switch (type)
        {
            // Quick Revive
            case 1:
                data.ChangeHealSpeed(-2.5f);
                break;
            // Juggernog
            case 2:
                data.ChangeMaxHealth(100f);
                break;
            // Speed Cola
            case 3:
                data.ChangeReloadSpeedMod(1f);
                break;
            // Double Tap
            case 4:
                data.ChangeDamageMod(1f);
                break;
        }
    }

    private bool IsDuplicate()
    {
        if (data.CheckDuplicatePerks(type))
        {
            return true;
        }
        return false;
    }

    public void RemovePanel()
    {
        panel.SetActive(false);
    }
}
