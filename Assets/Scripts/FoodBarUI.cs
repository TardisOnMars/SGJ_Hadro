using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FoodBarUI : MonoBehaviour
{

    public Image insideBar;
    public Image stomachIcon;
    public Color blinkColor = Color.red;
    private float _fillAmount = 1f;
    private Tween blinkingTween;

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
        if(blinkingTween == null)
            blinkingTween = stomachIcon.DOColor(blinkColor, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();
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
