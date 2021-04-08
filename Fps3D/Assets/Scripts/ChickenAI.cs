using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform player;
    private List<Transform> navSteps;
    private float detectionDistance = 5;
    private int destinationIndex = 0;
    private float walkSpeed = 1;
    private float runSpeed = 5;

    private Animator animator;
    private bool playerFound = false;

    private Vector3 normalScale;
    private Vector3 angryScale;

    void Start()
    {
        player = Player.Instance.transform;
        normalScale = transform.localScale;
        angryScale = normalScale * 4;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navSteps = new List<Transform>();
        foreach (GameObject navStep in GameObject.FindGameObjectsWithTag("NavStep"))
        {
            navSteps.Add(navStep.transform);
        }
        animator.SetBool("Walk", true);
    }

    void Update()
    {
        if (!agent.isStopped)
        {
            SearchPlayer();
            if (playerFound && transform.localScale.magnitude < angryScale.magnitude)
            {
                transform.localScale *= 1.05f;
            }
            if (!playerFound && transform.localScale.magnitude > normalScale.magnitude)
            {
                transform.localScale *= 0.95f;
            }
            CheckDestination();
        }
    }

    public void SearchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionDistance)
        {
            agent.destination = player.position;
            if (!playerFound)
            {
                playerFound = true;
                agent.speed = runSpeed;
                detectionDistance = 10;
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
            }
        }
        else
        {
            if (playerFound)
            {
                playerFound = false;
                agent.destination = navSteps[destinationIndex].position;
                agent.speed = walkSpeed;
                detectionDistance = 5;
                animator.SetBool("Run", false);
                animator.SetBool("Walk", true);
            }
        }
    }

    public void CheckDestination()
    {
        float dist = agent.remainingDistance;
        if (dist <= 0.05f)
        {
            Transform lastNavStep = navSteps[destinationIndex];
            navSteps.Remove(lastNavStep);
            navSteps.Add(lastNavStep);
            destinationIndex = Random.Range(0, navSteps.Count - 1);
            agent.destination = navSteps[destinationIndex].position;
        }
    }

    public IEnumerator StopAgent(float time)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(time);
        agent.isStopped = false;
    }
}
