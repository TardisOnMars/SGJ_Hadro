using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GraphicRaycaster graphicRaycaster;
    // Struct to hold pointer data (mainly its position)
    private PointerEventData _clickData;
    // List containing all the UI elements hit by the raycast
    private List<RaycastResult> _raycastResults;
    
    public LayerMask groundLayer;
    public InputActionReference moveAction;
    
    private NavMeshAgent _agent;

    private Tween _tweenIdle;

    [SerializeField] private Animator _animator;


    public void Start()
    {
        _clickData = new PointerEventData(EventSystem.current);
        _raycastResults = new List<RaycastResult>();
        _agent = GetComponent<NavMeshAgent>();
        moveAction.action.performed += SetDestination;
    }

    void Update()
    {
        if (_agent.velocity.magnitude > 0.1f)
        {
            _animator.SetBool("IsWalking",true);
            DOTween.Kill(_tweenIdle);
            _tweenIdle.SetLoops(0);
            _tweenIdle.Complete();
            _tweenIdle.Kill();
            _tweenIdle = null;
            transform.localScale = new Vector3(transform.localScale.x, 1, 1);

        }
        else
        {
            _animator.SetBool("IsWalking", false);
            if (_tweenIdle == null)
                _tweenIdle = transform.DOScaleY(transform.localScale.y + 0.03f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();
        }
    }

    public void SetDestination(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        // Check that user did not click over a UI element
        if (HasClickedOverUI())
        {
            return;
        }

        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _agent.SetDestination(destination);
        
        transform.localScale = _agent.destination.x < transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
    
    private bool HasClickedOverUI()
    {
        _clickData.position = Mouse.current.position.ReadValue();
        _raycastResults.Clear();
        graphicRaycaster.Raycast(_clickData, _raycastResults);
        return _raycastResults.Count > 0;
    }
}
