using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected int cost;
    protected string notificationText;
    protected PlayerData data;

    protected virtual void Init()
    {
    }

    public abstract void Interact();

    public void SubCost()
    {
        data.ChangePoints(-cost);
    }

    public bool CheckCost()
    {
        return (cost <= data.points);
    }

    public void SetPlayerData(PlayerData _data)
    {
        data = _data;
    }

    public virtual void SetNotificationText()
    {
        UIData.Instance.SetNotificationText(notificationText);
    }

    public void ClearNotificationText()
    {
        UIData.Instance.SetNotificationText(null);
    }
}
