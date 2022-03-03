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

    private GameManager gameManager;
    private InputManager inputManager;
    private Crosshair crosshair;
    private Bow bow;
    private Transform cam;

    [SerializeField]
    private Animator camAnimator;
    private UI ui;

    private bool isAiming = false;
    private bool enemyFound = false;
    private bool canBeHit = true;

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
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        inputManager = InputManager.Instance;
        crosshair = Crosshair.Instance;
        bow = GetComponentInChildren<Bow>();
        cam = Camera.main.transform;

        ui = UI.Instance;
        ui.SetMaxHealth(health);
        ui.SetNbArrowsText(bow.NbArrows);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            PowerUp powerUp = other.GetComponent<PowerUp>();
            bow.SetPowerUp(powerUp.GetPowerUp(), true);
            StartCoroutine(powerUp.Disable());
        }
        if (other.gameObject.tag == "ArrowSpot")
        {
            ArrowSpot arrowSpot = other.GetComponent<ArrowSpot>();
            bow.AddArrows(arrowSpot.GetNbArrows());
            ui.SetNbArrowsText(bow.NbArrows);
            StartCoroutine(arrowSpot.Disable());
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
                Vector3 force = (transform.position - collision.gameObject.transform.position) * 5;
                force.y = Mathf.Clamp(force.y, 0f, 2f);
                
                iTween.MoveAdd(gameObject, force, 0.5f);
                StartCoroutine(CanBeHit());
            }
            else
            {
                gameManager.EndGame(false);
            }
        }
    }

    public void Aim()
    {
        if (bow.ArrowSlotted)
        {
            isAiming = true;
            camAnimator.SetBool("isAiming", true);
            crosshair.Aim();
            bow.Aim();
        }
    }

    public void Shoot()
    {
        if (isAiming)
        {
            isAiming = false;
            camAnimator.SetBool("isAiming", false);
            crosshair.Shoot();
            bow.Shoot();
            StartCoroutine(PlayerHasShot());
        }
    }

    private IEnumerator PlayerHasShot()
    {
        inputManager.DisableShoot();
        yield return new WaitForSeconds(0.5f);
        inputManager.EnableShoot();

        ui.SetNbArrowsText(bow.NbArrows);
    }

    private IEnumerator CanBeHit()
    {
        yield return new WaitForSeconds(0.5f);
        canBeHit = true;
    }

}
