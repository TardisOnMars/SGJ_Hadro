using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GraphicRaycaster[] graphicRaycasters;
    // Struct to hold pointer data (mainly its position)
    private PointerEventData _clickData;
    // List containing all the UI elements hit by the raycast
    private List<RaycastResult> _raycastResults;
    
    public LayerMask groundLayer;
    public InputActionReference moveAction;
    
    private NavMeshAgent _agent;

    private Tween _tweenIdle;
    private Tween _tweenAttack;

    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _shoutSprite;
    [SerializeField] private SpriteRenderer _eyeBrowSprite;

    private Sequence attackSequence;

    public void Start()
    {
        _clickData = new PointerEventData(EventSystem.current);
        _raycastResults = new List<RaycastResult>();
        _agent = GetComponent<NavMeshAgent>();
        moveAction.action.performed += SetDestination;
    }

    void Update()
    {
        if (_agent.velocity.magnitude > 0.2f)
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
        graphicRaycasters = FindObjectsByType<GraphicRaycaster>(FindObjectsSortMode.None);
        _clickData.position = Mouse.current.position.ReadValue();
        _raycastResults.Clear();
        foreach(var raycaster in graphicRaycasters)
        {
            raycaster.Raycast(_clickData, _raycastResults);
        }
        return _raycastResults.Count > 0;
    }

    public void AttackAnim()
    {
        //if(attackSequence != null && attackSequence.IsPlaying())
        //{
        //    attackSequence.Kill();
        //}
        _tweenAttack.SetLoops(0);
        _tweenAttack.Complete();
        _tweenAttack.Kill();
        _tweenAttack = null;

        bool AnimWalkingValue = _animator.GetBool("IsWalking");
        if (AnimWalkingValue)
        {
            _animator.SetBool("IsWalking", false);
            _tweenIdle.SetLoops(0);
            _tweenIdle.Complete();
            _tweenIdle.Kill();
            _tweenIdle = null;
            transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        }

        _tweenAttack = this.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.2f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo)
            .OnPlay(() =>
        {
            _shoutSprite.enabled = true;
            _eyeBrowSprite.enabled = true;
        })
        .OnComplete(() =>
        {
                _shoutSprite.enabled = false;
                _eyeBrowSprite.enabled = false;

                transform.localScale = new Vector3(transform.localScale.x, 1, 1);

                if (AnimWalkingValue)
                    _animator.SetBool("IsWalking", true);
                else
                    _tweenIdle = transform.DOScaleY(transform.localScale.y + 0.03f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();

         }).Play();
        ;

        //if (attackSequence!=null  && ( attackSequence.IsPlaying() || !attackSequence.IsActive()))
        //    return;
        //attackSequence = DOTween.Sequence();


        //attackSequence.Append(transform.DOScaleY(transform.localScale.y, 0.1f)
        //    .SetEase(Ease.Linear)
        //    .SetLoops(2, LoopType.Yoyo)
        //    .OnPlay(() =>
        //    {
        //        _shoutSprite.enabled = true;
        //        _eyeBrowSprite.enabled = true;
        //    })
        //    .OnComplete(() =>
        //    {
        //        _shoutSprite.enabled = false;
        //        _eyeBrowSprite.enabled = false;

        //        transform.localScale = new Vector3(transform.localScale.x, 1, 1);

        //        if (AnimWalkingValue)
        //            _animator.SetBool("IsWalking", true);
        //        else
        //            _tweenIdle = transform.DOScaleY(transform.localScale.y + 0.03f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();

        //    }));
        //attackSequence.Join(transform.DOScaleX(transform.localScale.x, 0.1f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo));
        //attackSequence.Join(this.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.2f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo));


    }
}
