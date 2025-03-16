using UnityEngine;
using UnityEngine.AI;

public class FollowHadro : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform target;

    public bool isFollowing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Youngling " + collision.collider.name);
        if (collision.collider.CompareTag("MainHadro"))
        {
            isFollowing = true;
            target = collision.collider.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("MainHadro"))
        {
            isFollowing = true;
            target = collider.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            agent.SetDestination(target.position);
            transform.localScale = agent.destination.x < transform.position.x ? new Vector3(0.5f, 0.5f, 1) : new Vector3(-0.5f, 0.5f, 1);
        }
    }
}