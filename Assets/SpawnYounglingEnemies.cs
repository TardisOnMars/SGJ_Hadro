using UnityEngine;
using UnityEngine.AI;

public class SpawnYounglingEnemies : MonoBehaviour
{
    public GameObject younglingEnemyPrefab;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainHadro"))
        {
            var enemy = Instantiate(younglingEnemyPrefab);
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
            Destroy(gameObject);
        }
    }
}
