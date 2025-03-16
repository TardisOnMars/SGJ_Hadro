using UnityEngine;
using UnityEngine.AI;

public class SpawnYounglingEnemies : MonoBehaviour
{
    public GameObject younglingEnemyPrefab;
    void OnTriggerEnter2D(Collider2D other)
    {
        var value = Random.Range(0f, 100f);
        if (value > 25f) return;
        
        if (other.CompareTag("MainHadro"))
        {
            AudioManager.Instance.PlaySoundOneShoot("TroodonRicane");
            var enemy = Instantiate(younglingEnemyPrefab, this.transform.position, Quaternion.identity);
            GameManager.Instance.OnAddYounglingEnemy(enemy);
            
            if (GameManager.Instance.individuals.Count > 0)
            {
                var target = GameManager.Instance.individuals[^1];
                enemy.GetComponent<YounglingEnemy>().SetTarget(target.transform);
            }
            else
            {
                enemy.GetComponent<YounglingEnemy>().SetTarget(GameManager.Instance.mainHadro.transform);
            }
        }
    }
}
