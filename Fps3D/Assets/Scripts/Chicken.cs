using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private Transform powerUpEffectSpawner;
    [SerializeField]
    private Transform canvas;
    [SerializeField]
    private HealthBar healthBar;

    private Transform player;
    private ChickenAI chickenAI;

    private bool isDead = false;
    private bool canBeHit = true;
    private int health = 100;
    private int value = 200;
    private int strengh = 20;

    void Start()
    {
        player = Player.Instance.transform;
        chickenAI = GetComponent<ChickenAI>();
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        canvas.LookAt(player);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow" && canBeHit)
        {
            chickenAI.IsAttacked(true);
            int damage = Mathf.RoundToInt(collision.relativeVelocity.magnitude);
            TakeDamage(damage);
            if (!isDead)
            {
                GameObject powerUp = collision.gameObject.GetComponent<Arrow>().GetPowerUp();
                if (powerUp)
                {
                    GameObject powerUpEffect = Instantiate(powerUp, powerUpEffectSpawner);
                    powerUpEffect.transform.localScale *= 5;
                    ActivatePowerUpEffect(powerUpEffect);
                }
                canBeHit = false;
                StartCoroutine(CanBeHit());
            }
        }
    }

    private IEnumerator CanBeHit()
    {
        yield return new WaitForSeconds(0.5f);
        canBeHit = true;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Player.Instance.AddPoints(value);
            ChickenSpawner.Instance.ChickenKilled();
            Destroy(gameObject);
        }
        else
        {
            healthBar.SetHealth(health);
        }
    }

    private IEnumerator TakeDamageOverTime(int damage, int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(damage);
        }
    }

    private void ActivatePowerUpEffect(GameObject powerUpEffect)
    {
        int effectTime;
        switch (powerUpEffect.tag)
        {
            case "Fire":
                effectTime = 5;
                StartCoroutine(TakeDamageOverTime(10, effectTime));
                Destroy(powerUpEffect, effectTime);
                break;
            case "Lightning":
                effectTime = 3;
                StartCoroutine(chickenAI.StopAgent(effectTime));
                iTween.ShakeScale(body, new Vector3(0.5f, 0.5f, 0.5f), effectTime);
                Destroy(powerUpEffect, effectTime);
                break;
            default:
                break;
        }
    }

    public int GetStrengh()
    {
        return strengh;
    }
}
