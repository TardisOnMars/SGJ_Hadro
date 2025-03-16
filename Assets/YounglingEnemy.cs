using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class YounglingEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform _target;
    private bool _isHunting;
    private int _attacked;
    private List<FleeingTarget> _fleeingTargets;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _fleeingTargets = FindObjectsByType<FleeingTarget>(FindObjectsSortMode.InstanceID).ToList();
    }

    void Update()
    {
        if (_isHunting)
        {
            if (_target == null)
            {
                agent.SetDestination(_fleeingTargets[Random.Range(0, _fleeingTargets.Count-1)].transform.position);
            }
            else
            {
                agent.SetDestination(_target.position);
            }
        }
        else
        {
            agent.SetDestination(_fleeingTargets[Random.Range(0, _fleeingTargets.Count-1)].transform.position);
            agent.speed = agent.remainingDistance;
        }

    }

    public void SetTarget(Transform t)
    {
        _target = t;
        _isHunting = true;
    }

    public void OnAttacked()
    {
        _attacked++;
        if (_attacked >= 5 && _isHunting)
        {
            _isHunting = false;
            StartCoroutine(KillEnemy());
        }
    }

    IEnumerator KillEnemy()
    {
        yield return new WaitForSeconds(5f);
        GameManager.Instance.OnKillYounglingEnemy(gameObject);
        Destroy(gameObject);
    }
}
