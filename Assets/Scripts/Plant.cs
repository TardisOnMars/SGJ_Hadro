using System;
using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float fillAmount = 1f;
    public Sprite eatenSprite;
    public Sprite fullSprite;
    private SpriteRenderer spriteRenderer;
    private bool _isEaten = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool OnEaten()
    {
        if (!_isEaten)
        {
            spriteRenderer.sprite = eatenSprite;
            StartCoroutine(GrowBack());
            _isEaten = true;
            return true;
        }
        
        return false;
    }

    IEnumerator GrowBack()
    {
        yield return new WaitForSeconds(10f);
        spriteRenderer.sprite = fullSprite;
        _isEaten = false;
    }
}
