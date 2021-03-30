using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private int forceMultiplier = 20;
    private bool shot = false;
    private bool hasCollided = false;

    private Transform powerUpSpawner;
    private GameObject powerUp = null;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        powerUpSpawner = transform.GetChild(0);
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
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        shot = true;
        GetComponent<BoxCollider>().enabled = true;
        rb.AddForce(transform.forward * pulledForce * forceMultiplier);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            if (other.gameObject.tag == "Chicken")
            {
                transform.parent = other.gameObject.transform;
                other.gameObject.GetComponent<Rigidbody>().AddForce(rb.velocity / 2, ForceMode.Impulse);
            }
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.isKinematic = true;
            Destroy(gameObject, 5f);
        }
    }

    public void SetPowerUp(GameObject newPowerUp)
    {
        powerUp = newPowerUp;
        Instantiate(powerUp, powerUpSpawner);
    }

}
