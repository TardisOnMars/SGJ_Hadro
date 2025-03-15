using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Nest : MonoBehaviour
{
    
    private int _eggAmount;
    public bool _isBuilt;
    public bool _isBreeding;
    public bool _isBuilding;
    
    private Youngling[] _younglings;
    public Transform[] _hatchingPositions;

    public float _buildingSpeed = 0.3f;
    private float _buildDuration = 5f;
    public FillBar buildingFillBar;
    
    public FillBar breedingBar;
    private SpriteRenderer _nestSpriteRenderer;
    public Sprite[] nestSprites;


    public GameObject younglingPrefab;
    public float hatchingDuration = 8f;
    public float currentHatchingTime;
    private float _breedingSpeed =  0.3f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildingFillBar.FillAmount = 0f;
        breedingBar.FillAmount = 0f;
        
        _nestSpriteRenderer = GetComponent<SpriteRenderer>();
        _nestSpriteRenderer.sprite = nestSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isBuilt && _isBuilding)
        {
            buildingFillBar.FillAmount += Time.deltaTime * _buildingSpeed;
            if (buildingFillBar.FillAmount >= 1f)
            {
                _isBuilt = true;
                _isBuilding = false;
                buildingFillBar.gameObject.SetActive(false);
                buildingFillBar.FillAmount = 0f;
                SwitchNestSprite();
            }
        }

        if (!_isBuilt) return;
        
        if (_isBreeding && _eggAmount == 0)
        {
            breedingBar.FillAmount += _breedingSpeed * Time.deltaTime;
            if (breedingBar.FillAmount >= 1f)
            {
                _eggAmount = 3;
                SwitchNestSprite();
                breedingBar.gameObject.SetActive(false);
                breedingBar.FillAmount = 0f;
                _isBreeding = false;
            }
        }

        if (_eggAmount > 0)
        {
            currentHatchingTime += Time.deltaTime;
            if (currentHatchingTime >= hatchingDuration)
            {
                _eggAmount--;
                Debug.Log("Update" + _eggAmount);
                var youngling = Instantiate(younglingPrefab, _hatchingPositions[_eggAmount].position, Quaternion.identity);
                currentHatchingTime = 0f;
                SwitchNestSprite();
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("MainHadro")) return;
        Debug.Log("OnTriggerEnter2D" + _eggAmount);
        if (!_isBuilt)
        {
            _isBuilding = true;
            buildingFillBar.gameObject.SetActive(true);
        }
        else if (!_isBreeding && _eggAmount == 0)
        {
            
            _isBreeding = true;
            breedingBar.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        _isBuilding = false;
        _isBreeding = false;
        
    }
    
    public void SwitchNestSprite()
    {
        if (_eggAmount == 0)
        {
            _nestSpriteRenderer.sprite = nestSprites[1];
        }

        if (_eggAmount > 0)
        {
            _nestSpriteRenderer.sprite = nestSprites[2];
        }
    }
}
