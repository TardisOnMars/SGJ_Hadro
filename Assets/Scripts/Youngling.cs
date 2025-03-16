using UnityEngine;

public class Youngling : MonoBehaviour
{
    public GameObject bonesPile;
    public float currentTimerAudio = 0f;
    private float timeBeforeAudio = 0f;

    void Start()
    {
        AudioManager.Instance.PlaySoundOneShoot("EclosionOeuf");
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
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("YounglingEnemy"))
        {
            var younglingEnemy = other.GetComponent<YounglingEnemy>();
            if (!younglingEnemy.IsHunting()) return;
            
            Instantiate(bonesPile, transform.position, Quaternion.identity);
            GameManager.Instance.OnKillYoungling(gameObject);
            Destroy(gameObject);
        }
    }
}
