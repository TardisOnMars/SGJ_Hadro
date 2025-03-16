using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public LayerMask groundLayer;
    public InputActionReference moveAction;
    
    private NavMeshAgent _agent;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        moveAction.action.performed += ctx => SetDestination();
    }

    public void SetDestination()
    {
        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _agent.SetDestination(destination);
    }
}
