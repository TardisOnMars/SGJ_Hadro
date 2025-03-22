using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AdultHadro : MonoBehaviour
{
    
    public GameObject bonesPile;
    public bool isFollowing = true;
    public Transform target;

    private NavMeshAgent agent;
    private Tween _tweenIdle;

    [SerializeField] private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(CoWaitAddHadro());
        target = FindFirstObjectByType<MainHadro>().gameObject.transform;
    }

    private IEnumerator CoWaitAddHadro()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        GameManager.Instance.OnAddHadro(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude > 0.2f)
        {
            _animator.SetBool("IsWalking", true);
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

        if (isFollowing)
        {
            agent.SetDestination(target.position);
            transform.localScale = agent.destination.x < transform.position.x ? new Vector3(0.8f, 0.8f, 1) : new Vector3(-0.8f, 0.8f, 1);
        }
    }

    public void KillAdultHadro()
    {
        DOTween.Kill(_tweenIdle);
        _tweenIdle.SetLoops(0);
        _tweenIdle.Complete();
        _tweenIdle.Kill();
        _tweenIdle = null;
        Instantiate(bonesPile, transform.position, Quaternion.identity);
    }


}
