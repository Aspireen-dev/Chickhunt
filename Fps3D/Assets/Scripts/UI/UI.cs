using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    // ----- SINGLETON -----
    private static UI _instance;

    public static UI Instance
    {
        get
        {
            return _instance;
        }
    }

    // ----- PANELS -----
    [SerializeField]
    private MainPanel mainPanel;
    [SerializeField]
    private PausePanel pausePanel;
    [SerializeField]
    private EndGamePanel endGamePanel;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    public void SetMaxHealth(int health)
    {
        mainPanel.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        mainPanel.SetHealth(health);
    }

    public void SetTimeRemainingText(int time)
    {
        mainPanel.SetTimeRemainingText(time);
    }

    public void SetNbArrowsText(int nbArrows)
    {
        mainPanel.SetNbArrowsText(nbArrows);
    }

    public void SetNbChickenKilled(int nbChickens, int nbChickenToSpawn)
    {
        mainPanel.SetNbChickenKilled(nbChickens, nbChickenToSpawn);
    }

    public void EndGame()
    {
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.SetScoreText(Player.Instance.Score);
    }

    public void Pause()
    {
        pausePanel.gameObject.SetActive(true);
    }

    public void UnPause()
    {
        pausePanel.gameObject.SetActive(false);
    }
}
