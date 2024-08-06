using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : Interactable
{
    [SerializeField]
    private GameObject[] affectedObjects;

    private void Start()
    {
        base.Init();
        notificationText = "Hold 'F' to buy Mystery Box (Cost: " + cost + ")";
    }

    public override void Interact()
    {
        WeaponType[] w_data = UtilityData.Instance.GetWeaponDatabase().Weapons;
        int i = Random.Range(1, w_data.Length);
        while (data.CheckWeapon(w_data[i]))
        {
            i = Random.Range(1, w_data.Length);
        }
        StartCoroutine(LoadMysteryWeapon(data, w_data[i]));
        SubCost();
    }

    IEnumerator LoadMysteryWeapon(PlayerData _data, WeaponType w)
    {
        FlipBox(0, 1);
        yield return new WaitForSeconds(1f);
        _data.AddWeapon(w);
        FlipBox(0, 1);
    }

    private void FlipBox(int i, int j)
    {
        affectedObjects[i].SetActive(!affectedObjects[i].activeSelf);
        affectedObjects[j].SetActive(!affectedObjects[j].activeSelf);
    }
}
