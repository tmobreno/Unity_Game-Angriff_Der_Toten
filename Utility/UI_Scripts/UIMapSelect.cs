using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMapSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject components;

    public static UIMapSelect Instance { get; private set; }

    [SerializeField]
    private Button map1Button, map2Button, map3Button;

    void Start()
    {
        Instance = this;
    }

    public void SetActive(bool set)
    {
        components.SetActive(set);
    }

    public void LoadMap1()
    {
        SceneManager.LoadScene("Angriff_Der_Toten", LoadSceneMode.Single);
    }
}
