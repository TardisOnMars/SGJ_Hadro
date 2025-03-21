using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;

    private static GameManager _instance;

    public int eggCount = 0;
    public int younglingCount = 0;
    public int hadroCount = 0;
    public int individualTotalCount = 0;
    public int younglingEnemyCount = 0;
    public GameObject mainHadro;
    public List<GameObject> individuals = new();
    public List<GameObject> younglingEnemies = new();
    public int victoryAmount = 5;

    public UnityEvent addEgg = new();
    public UnityEvent killEgg= new();
    public UnityEvent addYoungling= new();
    public UnityEvent killYoungling= new();
    public UnityEvent addHadro= new();
    public UnityEvent killHadro= new();
    public UnityEvent addYounglingEnemy = new();
    public UnityEvent killYounglingEnemy = new();
    
    public UnityEvent checkPopulation = new();

    public Canvas attackButtonCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        checkPopulation.AddListener(OnCheckPopulation);
    }

    public void OnAddEgg()
    {
        eggCount++;
        addEgg.Invoke();
        checkPopulation.Invoke();
    }

    public void OnKillEgg()
    {
        eggCount--;
        killEgg.Invoke();
        checkPopulation.Invoke();
    }

    public void OnAddYoungling(GameObject youngling)
    {
        younglingCount++;
        individualTotalCount++;
        individuals.Add(youngling);
        addYoungling.Invoke();
        checkPopulation.Invoke();
    }

    public void OnKillYoungling(GameObject youngling)
    {
        younglingCount--;
        individualTotalCount--;
        killYoungling.Invoke();
        individuals.Remove(youngling);
        checkPopulation.Invoke();
    }

    public void OnAddHadro(GameObject hadro)
    {
        hadroCount++;
        individualTotalCount++;
        individuals.Add(hadro);
        addHadro.Invoke();
        checkPopulation.Invoke();
    }
    
    public void OnKillHadro(GameObject hadro)
    {
        hadroCount--;
        individualTotalCount--;
        if(individuals.Contains(hadro)) individuals.Remove(hadro);
        killHadro.Invoke();
        checkPopulation.Invoke();
    }

    public void OnAddYounglingEnemy(GameObject enemy)
    {
        younglingEnemyCount++;
        younglingEnemies.Add(enemy);
        addYounglingEnemy.Invoke();
        checkPopulation.Invoke();
    }
    
    public void OnKillYounglingEnemy(GameObject enemy)
    {
        younglingEnemyCount--;
        younglingEnemies.Remove(enemy);
        killYounglingEnemy.Invoke();
        checkPopulation.Invoke();
    }
    
    public void OnCheckPopulation()
    {
        if (individualTotalCount >= victoryAmount)
        {
            Debug.Log("Victory!");
            FindFirstObjectByType<SceneLoader>().LoadNextScene();
        }
        else if(individualTotalCount <= 0)
        {
            AudioManager.Instance.PlaySoundOneShoot("AdulteMort");
            Debug.Log("Game Over!");
            FindFirstObjectByType<SceneLoader>().LoadScene(9);
        }

        if (younglingEnemyCount > 0)
        {

            attackButtonCanvas.enabled = true;
        }
        else
        {
            attackButtonCanvas.enabled = false;
        }
    }


    public void OnAttack()
    {
        AudioManager.Instance.PlaySoundOneShoot("AdulteGrogne");
        foreach (var enemy in younglingEnemies)
        {
            if (Vector3.Distance(mainHadro.transform.position, enemy.transform.position) < 10f)
            {
                var younglingEnemy = enemy.GetComponent<YounglingEnemy>();
                younglingEnemy.OnAttacked(mainHadro);
            }
        }
    }

    public void OnDestroy()
    {
        DOTween.KillAll();
        DOTween.Clear();
    }
}
