using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;

    private static GameManager _instance;

    public int eggCount = 0;
    public int younglingCount = 0;
    public int hadroCount = 0;
    public int individualTotalCount = 0;
    public List<GameObject> individuals = new();
    public int victoryAmount = 5;

    public UnityEvent addEgg = new();
    public UnityEvent killEgg= new();
    public UnityEvent addYoungling= new();
    public UnityEvent killYoungling= new();
    public UnityEvent addHadro= new();
    public UnityEvent killHadro= new();
    
    public UnityEvent checkPopulation = new();


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

    public void OnCheckPopulation()
    {
        if (individualTotalCount >= victoryAmount)
        {
            Debug.Log("Victory!");
            FindFirstObjectByType<SceneLoader>().LoadScene(2);
        }
        else if(individualTotalCount <= 0)
        {
            Debug.Log("Game Over!");
            FindFirstObjectByType<SceneLoader>().LoadScene(3);
        }
    }
}
