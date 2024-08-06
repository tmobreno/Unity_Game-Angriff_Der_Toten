using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreboardData : MonoBehaviour
{
    private PlayerData data;

    [SerializeField]
    private GameObject components;

    [SerializeField]
    private Text userName, userScore, userKills;

    [SerializeField]
    private Image[] fuses;

    private int numFuses;

    public static UIScoreboardData Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        numFuses = 0;
        Instance = this;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        data = FindObjectOfType<PlayerData>();
    }

    public void SetActive(bool set)
    {
        if (UIPauseMenu.Instance.IsPaused) { return; }
        UIData.Instance.SetActive(!set);
        components.SetActive(set);
        int j = 0;
        foreach (Image i in fuses)
        {
            CheckFuse(i, j);
            j++;
        }
    }

    public void UpdateKills(int kills)
    {
        userKills.text = kills.ToString();
    }

    public void UpdateScore(int score)
    {
        userScore.text = score.ToString();
    }

    public void UpdateFuses(int fuses)
    {
        numFuses = fuses;
    }

    public void CheckFuse(Image i, int j)
    {
        if (numFuses > j)
        {
            i.enabled = true;
            return;
        }
        i.enabled = false;
    }
}
