using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject components;

    public static UIMainMenu Instance { get; private set; }

    [SerializeField]
    private Button playButton, optionsButton, quitButton;
    
    void Start()
    {
        Instance = this;
    }

    public void LoadMapSelect()
    {
        this.SetActive(false);
        UIMapSelect.Instance.SetActive(true);
    }

    public void SetActive(bool set)
    {
        components.SetActive(set);
    }
}
