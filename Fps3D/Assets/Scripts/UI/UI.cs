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

    public MainPanel mainPanel;

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
        mainPanel = GetComponentInChildren<MainPanel>();
    }

    public void Aim()
    {
        mainPanel.Aim();
    }

    public void Shoot()
    {
        mainPanel.Shoot();
    }

}
