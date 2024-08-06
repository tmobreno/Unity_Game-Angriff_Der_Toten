using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIData : MonoBehaviour
{
    private PlayerData data;
    private ZoneControl zoneC;
    [SerializeField]
    private Text ammoCount, grenadeCount, pointCount, roundCount, notification;

    [SerializeField]
    private GameObject components;

    public static UIData Instance { get; private set; }

    void Start()
    {
        Instance = this;
        InitializeComponents();
    }

    private void Update()
    {
        UpdateAmmoCount();
        UpdatePointCount();
        UpdateRoundCount();
    }

    private void InitializeComponents()
    {
        data = FindObjectOfType<PlayerData>();
        zoneC = FindObjectOfType<ZoneControl>();
    }

    public void UpdateAmmoCount()
    {
        ammoCount.text = data.GetEquippedWeapon().type.Name + "   " +
            data.GetEquippedWeapon().remainingAmmo + " / " + 
            data.GetEquippedWeapon().storedAmmo;
    }

    public void UpdatePointCount()
    {
        pointCount.text = data.points.ToString();
    }

    public void UpdateRoundCount()
    {
        roundCount.text = zoneC.roundCounter.ToString();
    }

    public void SetNotificationText(string text)
    {
        notification.text = text;
    }

    public void SetActive(bool set)
    {
        components.SetActive(set);
    }

    public void UpdateGrenadeCount(int grenades)
    {
        string s = "";
        for(int i = 0; i < grenades; i++)
        {
            s += " O";
        }
        grenadeCount.text = s;
    }
}
