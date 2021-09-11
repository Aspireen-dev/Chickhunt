using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenAI : MonoBehaviour
{
    public List<Transform> navSteps;

    private NavMeshAgent agent;

    private Transform player;
    private int destinationIndex;
    private float detectionDistance = 10;
    private float walkSpeed = 1;
    private float runSpeed = 5;

    private Animator animator;
    private bool playerFound = false;

    private Vector3 normalScale;
    private Vector3 angryScale;

    private bool isAttacked = false;

    void OnEnable()
    {
        player = Player.Instance.transform;
        normalScale = transform.localScale;
        angryScale = normalScale * 3;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (navSteps != null)
        {
            destinationIndex = Random.Range(0, navSteps.Count);
            animator.SetBool("Walk", true);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void Update()
    {
        if (!agent.isStopped)
        {
            if (isAttacked)
            {
                TargetPlayer();
            }
            else
            {
                SearchPlayer();
            }
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
            TargetPlayer();
        }
        else
        {
            if (playerFound)
            {
                playerFound = false;
                agent.destination = navSteps[destinationIndex].position;
                agent.speed = walkSpeed;
                animator.SetBool("Run", false);
                animator.SetBool("Walk", true);
            }
        }
    }

    void TargetPlayer()
    {
        agent.destination = player.position;
        if (!playerFound)
        {
            playerFound = true;
            agent.speed = runSpeed;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        }
    }

    void CheckDestination()
    {
        float dist = agent.remainingDistance;
        if (dist <= 0.2f)
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

    public void IsAttacked(bool attacked)
    {
        isAttacked = attacked;
    }
}
