using UnityEngine;
using UnityEngine.AI;

public class SpawnYounglingEnemies : MonoBehaviour
{
    public GameObject younglingEnemyPrefab;
    public Transform spawnPoint;

    private float spawnChance = 45f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "YounglingEnemy") return;

        var value = Random.Range(0f, 100f);
        //Debug.Log("Spawn chance = " + spawnChance + " value = " + value + " other name " + other.name);
        if (value < spawnChance)
        {
            spawnChance -= 30f;
            return;
        }
        else
        {
            spawnChance += 15f;
        }

        if (other.CompareTag("MainHadro"))
        {
            AudioManager.Instance.PlaySoundOneShoot("TroodonRicane");
            //var enemy = Instantiate(younglingEnemyPrefab, this.transform.position, Quaternion.identity);
            var enemy = Instantiate(younglingEnemyPrefab, spawnPoint.position, Quaternion.identity);
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
