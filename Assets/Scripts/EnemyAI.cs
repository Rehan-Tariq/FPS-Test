using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float health = 50f;
    public float attackRange = 15f;
    public Transform[] coverPoints;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < attackRange)
        {
            agent.SetDestination(player.position);
            ShootPlayer();
        }
        else
        {
            TakeCover();
        }
    }

    void TakeCover()
    {
        Transform bestCover = coverPoints[Random.Range(0, coverPoints.Length)];
        agent.SetDestination(bestCover.position);
    }

    void ShootPlayer()
    {
        Debug.DrawRay(transform.position, player.position - transform.position, Color.red);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
