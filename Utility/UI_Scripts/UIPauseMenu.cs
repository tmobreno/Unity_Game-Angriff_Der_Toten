using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject components;

    public static UIPauseMenu Instance { get; private set; }

    public bool IsPaused { get; private set; }

    public bool CanPause { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        CanPause = true;
    }

    public void SetPaused(bool set)
    {
        IsPaused = set;
        UIScoreboardData.Instance.SetActive(false);
        UIData.Instance.SetActive(!set);
        components.SetActive(set);
        if(set == true) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public void SetCanPause(bool set)
    {
        CanPause = set;
    }
}
