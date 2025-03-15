using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class MainHadro : MonoBehaviour
{
    public const float MAX_FOOD = 100;
    
    public float food = 100;
    public float decreaseRateMoving = 0.1f;
    public float decreaseRateBreeding = 0.3f;
    public bool isBreeding = false;

    public FillBar foodBar;

    public List<GameObject> individuals = new();

    private bool isStarving;
    private Coroutine coStarving;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.checkPopulation.AddListener(UpdateIndividuals);
    }


    private void UpdateIndividuals()
    {
        foreach (var individual in GameManager.Instance.individuals)
        {
            if (!individuals.Contains(individual))
            {
                individuals.Add(individual);
            }
        }
    }
    private IEnumerator CoStarving()
    {
        while (GameManager.Instance.individualTotalCount > 0)
        {
            yield return new WaitForSeconds(4);
            if (individuals.Count == 0)
            {
                GameManager.Instance.OnKillHadro();
            }
            else
            {
                var toKill = individuals[0];
                individuals.Remove(toKill);

                if (toKill.GetComponent<Youngling>())
                {
                    GameManager.Instance.OnKillYoungling();
                }
                else if (toKill.GetComponent<Hadro>())
                {
                    GameManager.Instance.OnKillHadro();
                }
                Destroy(toKill);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBreeding)
        {
            food -= decreaseRateBreeding * Time.deltaTime * MAX_FOOD;
        }
        else
        {
            food -= decreaseRateMoving * Time.deltaTime * MAX_FOOD;
        }

        foodBar.FillAmount = Mathf.Clamp(food / MAX_FOOD, 0f, 1f);

        if (foodBar.FillAmount <= 0 && !isStarving)
        {
            foodBar.FillAmount = 0;
            isStarving = true;
            coStarving = StartCoroutine(CoStarving());
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Collectible"))
        {
            //TOO foutre un delay pour l'anim
            EatFood(other.GetComponent<Plant>());
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter "+other.gameObject.name);
    }
    public void EatFood(Plant food)
    {
        this.food += food.fillAmount * 100;
        if (this.food > MAX_FOOD) this.food = MAX_FOOD;
        
        foodBar.FillAmount = Mathf.Clamp(this.food / MAX_FOOD, 0f, 1f);
        isStarving = false;
        if(coStarving != null) StopCoroutine(coStarving);
        Destroy(food.gameObject);
    }
}
