using UnityEngine;

public class Youngling : MonoBehaviour
{
    public GameObject bonesPile;

    void Start()
    {
        AudioManager.Instance.PlaySoundOneShoot("EclosionOeuf");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("YounglingEnemy"))
        {
            Instantiate(bonesPile, transform.position, Quaternion.identity);
            GameManager.Instance.OnKillYoungling(gameObject);
            Destroy(gameObject);
        }
    }
}
