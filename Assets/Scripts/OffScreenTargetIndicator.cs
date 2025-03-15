using UnityEngine;
using System.Collections.Generic;

public class OffScreenTargetIndicator : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject indicatorPrefab;
    
    private SpriteRenderer _spriteRenderer;
    private float _spriteWidth;
    private float _spriteHeight;
    
    private Camera _camera;

    private Dictionary<GameObject, GameObject> _targetIndicators = new();

    private void Start()
    {
        _camera = Camera.main;
        _spriteRenderer = indicatorPrefab.GetComponent<SpriteRenderer>();
        
        var bounds = _spriteRenderer.bounds;
        _spriteWidth = bounds.size.x / 2f;
        _spriteHeight = bounds.size.y / 2f;

        foreach (var target in targets)
        {
            var indicator = Instantiate(indicatorPrefab);
            indicator.SetActive(false);
            _targetIndicators.Add(target, indicator);
        }
    }

    private void Update()
    {
        foreach (KeyValuePair<GameObject, GameObject> entry in _targetIndicators)
        {
            var target = entry.Key;
            var indicator = entry.Value;
            UpdateTarget(target, indicator);
        }
    }

    private void UpdateTarget(GameObject target, GameObject indicator)
    {
        var screenPos = _camera.WorldToViewportPoint(target.transform.position);
        var isOffScreen = screenPos.x <= 0 || screenPos.x >= 1 || screenPos.y <= 0 || screenPos.y >= 1;

        if (isOffScreen)
        {
            indicator.SetActive(true);
            var spriteSizeInViewport = _camera.WorldToViewportPoint(new Vector3(_spriteWidth, _spriteHeight, 0f)) - _camera.WorldToViewportPoint(Vector3.zero);;
            
            screenPos.x = Mathf.Clamp(screenPos.x, spriteSizeInViewport.x, 1 - spriteSizeInViewport.x);
            screenPos.y = Mathf.Clamp(screenPos.y, spriteSizeInViewport.y, 1 - spriteSizeInViewport.y);
            
            var worldPosition = _camera.ViewportToWorldPoint(screenPos);
            worldPosition.z = 0f;
            indicator.transform.position = worldPosition;
            
            var direction = target.transform.position - indicator.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicator.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        }
        else
        {
            indicator.SetActive(false);
        }
        
    }
    
}
