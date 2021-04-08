using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private static UI _instance;

    public static UI Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private MainPanel mainPanel;
    [SerializeField]
    private EndGamePanel endGamePanel;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        HideCursor();
    }

    public void SetMaxHealth(int health)
    {
        mainPanel.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        mainPanel.SetHealth(health);
    }
    
    public void SetScoreText(int score)
    {
        mainPanel.SetScoreText(score);
    }

    public void EndGame(int score)
    {
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.SetScoreText(score);
        ShowCursor();
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
