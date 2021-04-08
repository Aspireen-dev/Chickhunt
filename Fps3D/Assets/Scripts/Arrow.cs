using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private int forceMultiplier = 20;
    private bool shot = false;
    private bool hasCollided = false;

    private GameObject powerUp = null;
    private Transform powerUpSpawner;
    private Transform trail;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        powerUpSpawner = transform.GetChild(0);
        trail = transform.GetChild(1);
    }

    void Update()
    {
        if (shot && rb.velocity.magnitude > Vector3.zero.magnitude)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void Shoot(float pulledForce)
    {
        shot = true;
        GetComponent<BoxCollider>().enabled = true;
        trail.GetComponent<TrailRenderer>().enabled = true;

        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.AddForce(transform.forward * pulledForce * forceMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.tag != "PowerUp")
        {
            hasCollided = true;
            trail.GetComponent<TrailRenderer>().enabled = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.isKinematic = true;
            transform.parent = collision.gameObject.transform;
            Destroy(gameObject, 5f);
        }
    }

    public GameObject GetPowerUp()
    {
        return powerUp;
    }

    public void SetPowerUp(GameObject newPowerUp)
    {
        if (powerUp != null)
        {
            Destroy(powerUp);
        }
        powerUp = Instantiate(newPowerUp, powerUpSpawner);
    }

}
