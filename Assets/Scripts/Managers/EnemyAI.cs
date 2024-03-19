using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float sightRange = 10f;
    public Transform player;
    private NavMeshAgent agent;
    public float wanderRadius = 5f;
    public float wanderTimer = 5f;
    [SerializeField] LayerMask playerLayerMask;

    public EnemyMovement enemyAttack;
    public Animator enemy;

    private float timer;
    private bool isWandering = false;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        StartWandering();
        gameObject.SetActive(true);

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {

        if (IsPlayerVisible())
        {
            Debug.Log("spotted");
            StopWandering();
            agent.SetDestination(player.position);
            enemyAttack.MoveTowardsPlayer();
        }
        else
        {

         StartWandering();
       }

    }


    bool IsPlayerVisible()
    {

        Vector3 directionToPlayer = player.position - transform.position;


        if (directionToPlayer.magnitude <= sightRange)
        {
            Debug.Log("1");
            Debug.DrawRay(transform.position, directionToPlayer.normalized * sightRange, Color.green);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, sightRange, playerLayerMask))
            {

                Debug.Log("2");

                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("3");
                    return true;
                }
            }
        }

        return false;
    }

    void StartWandering()
    {
        if (!isWandering)
        {
            isWandering = true;
            timer = wanderTimer;
        }

        if (timer <= 0)
        {

            Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
            randomPoint += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, 1);
            Vector3 finalPosition = hit.position;


            agent.SetDestination(finalPosition);
            timer = wanderTimer;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void StopWandering()
    {
        
        isWandering = false;
    }
}