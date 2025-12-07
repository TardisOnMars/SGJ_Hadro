using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Plant : MonoBehaviour
{
    [Header("Plant Attributes")]
    [Tooltip("Which amount (in %) of the Hadrosaurus feed bar can be regrow by eating this plant")]
    public float fillAmount = 1f;
    [Tooltip("Time before the plant regrows")]
    public float regrowDuration = 25f;

    [Space(3)]
    [Header("Sprites")]
    public Sprite eatenSprite;
    public Sprite fullSprite;
    public SpriteRenderer shadowSpriteRenderer;

    [Space(3)]
    [Header("Book Information")]
    public string pageName;

    public UnityEvent onPlantEaten;

    private SpriteRenderer spriteRenderer;
    private bool _isEaten = false;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public bool OnEaten()
    {
        if (!_isEaten)
        {
            onPlantEaten.Invoke();
            AudioManager.Instance.PlaySoundOneShoot("MangerPlante");
            shadowSpriteRenderer.gameObject.SetActive(false);
            spriteRenderer.sprite = eatenSprite;
            StartCoroutine(GrowBack());
            _isEaten = true;
            return true;
        }
        
        return false;
    }

    IEnumerator GrowBack()
    {
        yield return new WaitForSeconds(regrowDuration);
        spriteRenderer.sprite = fullSprite;
        shadowSpriteRenderer.gameObject.SetActive(true);
        _isEaten = false;
    }
}
