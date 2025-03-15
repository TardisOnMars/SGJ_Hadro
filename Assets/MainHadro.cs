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
}
