using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Interactable
{
    [SerializeField]
    private GameObject[] window = new GameObject[4];
    public int windowHealth;

    // Start is called before the first frame update
    void Start()
    {
        windowHealth = 2;
        if (window.Length == 0)
        {
            windowHealth -= 2;
        }
        base.Init();
        notificationText = "Hold 'F' to rebuild window";
    }

    void Update()
    {
        if (windowHealth > 0)
        {
            window[3].SetActive(true);
        }
        else
        {
            window[3].SetActive(false);
        }
    }

    public override void Interact()
    {
        if (windowHealth < 2)
        {
            windowHealth += 1;
            window[windowHealth].gameObject.SetActive(true);
            data.ChangePoints(10);
        }
    }

    public int GetWindowHealth()
    {
        return windowHealth;
    }

    public void ChangeWindowHealth(int change)
    {
        windowHealth += change;
        if (windowHealth < 0)
        {
            windowHealth = 0;
        }
    }

    public GameObject GetTarget()
    {
        return window[0];
    }

    public void DestroyBoard(int index)
    {
        if(index > 0) { window[index].SetActive(false); }
    }
}
