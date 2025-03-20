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

    public FoodBarUI foodBar;

    public List<GameObject> individuals = new();

    private bool isStarving;
    public float starvationDuration = 4f;
    private float currentStarvationTime = 0f;
    public GameObject bonesPrefab;
    


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
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("food " + food);
        if (isBreeding)
        {
            food -= decreaseRateBreeding * Time.deltaTime * MAX_FOOD;
        }
        else
        {
            food -= decreaseRateMoving * Time.deltaTime * MAX_FOOD;
        }
        this.food = Mathf.Clamp(this.food, 0f, MAX_FOOD);

        foodBar.FillAmount = Mathf.Clamp(food / MAX_FOOD, 0f, 1f);
        //Debug.Log("foodBar.FillAmount " + foodBar.FillAmount);

        if (foodBar.FillAmount <= 0 && !isStarving)
        {
            foodBar.FillAmount = 0;
            isStarving = true;
        }

        if (isStarving)
        {
            //AudioManager.Instance.PlaySoundOneShoot("AdulteAffame");
            currentStarvationTime += Time.deltaTime;
            
            if (currentStarvationTime >= starvationDuration)
            {
                currentStarvationTime = 0f;
                
                if (individuals.Count <= 0 )
                {
                    GameManager.Instance.OnKillHadro(gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    var toKill = individuals[^1];

                    if (toKill.GetComponent<Youngling>())
                    {
                        GameManager.Instance.OnKillYoungling(toKill);
                    }
                    else if (toKill.GetComponent<Hadro>())
                    {
                        GameManager.Instance.OnKillHadro(toKill);
                    }
                    var position = new Vector3(toKill.transform.position.x, toKill.transform.position.y, toKill.transform.position.z);
                    individuals.Remove(toKill);
                    Destroy(toKill);
                    Instantiate(bonesPrefab, position, Quaternion.identity);
                }
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collectible"))
        {
            EatFood(other.GetComponent<Plant>());
        }
    }

    public void EatFood(Plant food)
    {
        if (!food.OnEaten()) return;
        
        this.food += food.fillAmount * 100 / (GameManager.Instance.hadroCount + GameManager.Instance.younglingCount / 2.0f);
        //if (this.food > MAX_FOOD) this.food = MAX_FOOD;
        this.food = Mathf.Clamp(this.food, 0f, MAX_FOOD);

        foodBar.FillAmount = Mathf.Clamp(this.food / MAX_FOOD, 0f, 1f);
        isStarving = false;
        currentStarvationTime = 0f;
    }
}
