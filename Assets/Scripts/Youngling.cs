using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Youngling : MonoBehaviour
{
    public GameObject bonesPile;
    public float currentTimerAudio = 0f;
    private float timeBeforeAudio = 0f;

    private NavMeshAgent agent;
    private Animator _animator;
    private Tween _tweenIdle;

    void Start()
    {
        AudioManager.Instance.PlaySoundOneShoot("EclosionOeuf");
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        timeBeforeAudio = Random.Range(8f, 20f);
    }

    void Update()
    {
        currentTimerAudio += Time.deltaTime;
        if (currentTimerAudio > timeBeforeAudio)
        {
            AudioManager.Instance.PlaySoundOneShoot("PetitAmbiance" + Random.Range(1, 4).ToString());
            timeBeforeAudio = Random.Range(8f, 20f);
            currentTimerAudio = 0f;
        }

        if (agent.velocity.magnitude > 0.2f)
        {
            _animator.SetBool("IsWalking", true);
            DOTween.Kill(_tweenIdle);
            _tweenIdle.SetLoops(0);
            _tweenIdle.Complete();
            _tweenIdle.Kill();
            _tweenIdle = null;
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, 1);

        }
        else
        {
            _animator.SetBool("IsWalking", false);
            if (_tweenIdle == null)
                _tweenIdle = transform.DOScaleY(transform.localScale.y + 0.05f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("YounglingEnemy"))
        {
            var younglingEnemy = other.GetComponent<YounglingEnemy>();
            if (!younglingEnemy.IsHunting()) return;
            
            younglingEnemy.DepopAfterKill();
            Instantiate(bonesPile, transform.position, Quaternion.identity);
            GameManager.Instance.OnKillYoungling(gameObject);
            Destroy(gameObject);
        }
    }
}
