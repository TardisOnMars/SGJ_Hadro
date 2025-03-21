using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodBarUI : MonoBehaviour
{

    public Image insideBar;
    public Image stomachIcon;
    public Color blinkColor = Color.red;
    private float _fillAmount = 1f;
    private Tween blinkingTween;

    private Coroutine shoutCoroutine;
    public float FillAmount
    {
        get => _fillAmount;
        set
        {
            _fillAmount = value;
            insideBar.fillAmount = value;
            if(insideBar.fillAmount <= 0)
            {
                StartBlinking();
            }
            else
            {
                StopBlinking();
            }
        }
    }

    [ContextMenu("StartBlink")]
    public void StartBlinking()
    {
        if (shoutCoroutine == null)
            shoutCoroutine = StartCoroutine(CoShoutHunger());
        if (blinkingTween == null)
            blinkingTween = stomachIcon.DOColor(blinkColor, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();
    }

    private IEnumerator CoShoutHunger()
    {
        while (true)
        {
            if(_fillAmount <= 0f) 
                AudioManager.Instance.PlaySoundOneShoot("AdulteAffame");
            yield return new WaitForSeconds(3f);
        }
    }

    [ContextMenu("StopBlinking")]
    public void StopBlinking()
    {
        DOTween.Kill(blinkingTween);
        blinkingTween.SetLoops(0);
        blinkingTween.Complete();
        blinkingTween.Kill();
        stomachIcon.color = Color.white;
        blinkingTween = null;
    }

}
