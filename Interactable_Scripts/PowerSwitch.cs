using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PowerSwitch : Interactable
{
    [SerializeField]
    private GameObject[] affectedObjects;
    private ColorGrading color = null;

    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        notificationText = "Hold 'F' to turn on the power";
    }

    public override void Interact()
    {
        if (!activated)
        {
            PostProcessVolume vol = FindObjectOfType<Camera>().GetComponent<PostProcessVolume>();
            vol.profile.TryGetSettings(out color);
            color.saturation.value = 25;
            data.SetPower(true);
            FlipSwitch(0, 1);
            RemovePanels();
            activated = true;
            ClearNotificationText();
        }
    }

    public override void SetNotificationText()
    {
        if (!activated)
        {
            base.SetNotificationText();
        }
    }

    private void RemovePanels()
    {
        PerkMachine[] perks = GameObject.FindObjectsOfType<PerkMachine>();
        foreach (PerkMachine perk in perks)
        {
            perk.RemovePanel();
        }
    }

    private void FlipSwitch(int i, int j)
    {
        affectedObjects[i].SetActive(!affectedObjects[i].activeSelf);
        affectedObjects[j].SetActive(!affectedObjects[j].activeSelf);
    }
}
