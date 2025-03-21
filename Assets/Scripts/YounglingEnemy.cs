using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class YounglingEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform _target;
    private bool _isHunting;
    private int _attacked;
    private List<FleeingTarget> _fleeingTargets;
    public GameObject fleeIcon;

    private Vector3 fleeDestination;
    private float fleeSpeed = 12f;

    void Start()
    {
        EnemyIndicatorManager.Instance.AddTarget(this.gameObject);
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
            transform.localScale = agent.destination.x < transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,fleeDestination, fleeSpeed * Time.deltaTime);
            transform.localScale = fleeDestination.x < transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

            //fleeIcon.SetActive(true);
            //agent.SetDestination(_fleeingTargets[Random.Range(0, _fleeingTargets.Count-1)].transform.position);
            //agent.speed = 30f;
        }

    }

    public bool IsHunting()
    {
        return _isHunting;
    }
    public void SetTarget(Transform t)
    {
        _target = t;
        _isHunting = true;
    }

    public void OnAttacked(GameObject mainHadro)
    {
        _attacked++;
        if (_attacked >= 5 && _isHunting)
        {
            _isHunting = false;
            StartCoroutine(KillEnemy(mainHadro));
        }
    }

    IEnumerator KillEnemy(GameObject mainHadro)
    {
        fleeIcon.SetActive(true);
        agent.enabled = false;
        fleeDestination = mainHadro.transform.localPosition - this.transform.position *30;
        yield return new WaitForSeconds(1.5f);
        EnemyIndicatorManager.Instance.RemoveTarget(this.gameObject);
        GameManager.Instance.OnKillYounglingEnemy(gameObject);
        Destroy(gameObject);
    }

    public void DepopAfterKill()
    {
        EnemyIndicatorManager.Instance.RemoveTarget(this.gameObject);
        GameManager.Instance.OnKillYounglingEnemy(gameObject);
        Destroy(gameObject);
    }
}
