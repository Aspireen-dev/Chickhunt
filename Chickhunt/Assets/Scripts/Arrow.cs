using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private GameObject powerUpSpawner;
    [SerializeField]
    private GameObject trail;

    private GameObject powerUp = null;
    private Rigidbody rb;

    private int forceMultiplier = 30;
    private bool hasBeenShot = false;
    private bool hasCollided = false;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If the arrow has been shot and is still in movement
        if (hasBeenShot && rb.velocity.magnitude > Vector3.zero.magnitude)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void Shoot(float pulledForce)
    {
        hasBeenShot = true;
        GetComponent<BoxCollider>().enabled = true;
        trail.GetComponent<TrailRenderer>().enabled = true;

        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.AddForce(transform.forward * pulledForce * forceMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Limited to one collision
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

    // Used by the enemy to know if there is a power up active on the arrow
    public GameObject GetPowerUp()
    {
        return powerUp;
    }

    public void SetPowerUp(GameObject newPowerUp)
    {
        // Destroy the power up active if there is currently one
        if (powerUp != null)
        {
            Destroy(powerUp);
        }
        powerUp = Instantiate(newPowerUp, powerUpSpawner.transform);
    }

}
