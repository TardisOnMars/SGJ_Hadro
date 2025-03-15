using Unity.VisualScripting;
using UnityEngine;

public class MainHadro : MonoBehaviour
{
    public const float MAX_FOOD = 100;
    
    public float food = 100;
    public float decreaseRateMoving = 0.1f;
    public float decreaseRateBreeding = 0.3f;
    public bool isBreeding = false;

    public FillBar foodBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        Destroy(food.gameObject);
    }
}
