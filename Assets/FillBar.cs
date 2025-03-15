using UnityEngine;

public class FillBar : MonoBehaviour
{
    public Transform mask;
    public float minPos = 0f;
    public float maxPos = 5f;
    private float _fillAmount = 1f;
    public float FillAmount
    {
        get => _fillAmount;
        set
        {
            _fillAmount = value;
            mask.localPosition = new Vector3((maxPos - minPos) * _fillAmount, mask.localPosition.y, mask.localPosition.z);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [ContextMenu("Fill Amount")]
    public void TestFillAmount()
    {
        FillAmount = 0.5f;
    }
}
