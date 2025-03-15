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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MainHadro>())
        {
            isFollowing = true;
            target = other.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing)
            agent.SetDestination(target.position);
    }
}
