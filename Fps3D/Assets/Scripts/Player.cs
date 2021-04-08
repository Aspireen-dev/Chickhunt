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
    private Crosshair crosshair;
    private Bow bow;
    private Transform cam;
    private UI ui;

    private bool isAiming = false;
    private bool enemyFound = false;
    private bool canBeHit = true;

    private int score = 0;
    private int health = 100;

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
        inputManager = InputManager.Instance;
        crosshair = Crosshair.Instance;
        bow = GetComponentInChildren<Bow>();
        cam = Camera.main.transform;

        ui = UI.Instance;
        ui.SetMaxHealth(health);
        ui.SetScoreText(score);
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 20))
        {
            if (!enemyFound && hit.transform.gameObject.tag == "Chicken")
            {
                enemyFound = true;
                crosshair.UpdateCrosshair(enemyFound);
            }
            else if (enemyFound && hit.transform.gameObject.tag != "Chicken")
            {
                enemyFound = false;
                crosshair.UpdateCrosshair(enemyFound);
            }
        }
        else if (enemyFound)
        {
            enemyFound = false;
            crosshair.UpdateCrosshair(enemyFound);
        }
    }

    public void Aim()
    {
        isAiming = true;
        crosshair.Aim();
        bow.Aim();
    }

    public void Shoot()
    {
        if (isAiming)
        {
            isAiming = false;
            crosshair.Shoot();
            bow.Shoot();
            StartCoroutine(PlayerHasShot());
        }
    }

    private IEnumerator PlayerHasShot()
    {
        inputManager.DisableShoot();
        yield return new WaitForSeconds(1f);
        inputManager.EnableShoot();
    }

    private IEnumerator CanBeHit()
    {
        yield return new WaitForSeconds(0.5f);
        canBeHit = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Chicken" && canBeHit)
        {
            canBeHit = false;
            int damage = collision.gameObject.GetComponent<Chicken>().GetStrengh();
            health -= damage;
            ui.SetHealth(health);
            if (health > 0)
            {
                iTween.MoveAdd(gameObject, Vector3.back * 5, 0.5f);
                StartCoroutine(CanBeHit());
            }
            else
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        ui.EndGame(score);
    }

    public void ChickenKilled(int points)
    {
        score += points;
        ui.SetScoreText(score);
    }

}
