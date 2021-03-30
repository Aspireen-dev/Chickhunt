using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

    private InputManager inputManager;
    private UI ui;
    private Bow bow;

    private bool isAiming = false;

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
        Application.targetFrameRate = 80;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputManager = InputManager.Instance;
        ui = UI.Instance;
        bow = GetComponentInChildren<Bow>();
    }

    public void Aim()
    {
        isAiming = true;
        ui.Aim();
        bow.Aim();
    }

    public void Shoot()
    {
        if (isAiming)
        {
            isAiming = false;
            ui.Shoot();
            bow.Shoot();
            StartCoroutine(PlayerHasShot());
        }
    }

    IEnumerator PlayerHasShot()
    {
        inputManager.DisableShoot();
        yield return new WaitForSeconds(1f);
        inputManager.EnableShoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            GameObject powerUp = other.GetComponent<PowerUp>().GetPowerUp();
            bow.SetPowerUp(powerUp);
            Destroy(other.gameObject);
        }
    }

}
