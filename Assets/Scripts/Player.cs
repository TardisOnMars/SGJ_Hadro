using System;
using System.Collections.Generic;
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

    public void Start()
    {
        _clickData = new PointerEventData(EventSystem.current);
        _raycastResults = new List<RaycastResult>();
        _agent = GetComponent<NavMeshAgent>();
        moveAction.action.performed += SetDestination;
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
    }
    
    private bool HasClickedOverUI()
    {
        _clickData.position = Mouse.current.position.ReadValue();
        _raycastResults.Clear();
        graphicRaycaster.Raycast(_clickData, _raycastResults);
        return _raycastResults.Count > 0;
    }
}
