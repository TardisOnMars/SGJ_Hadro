using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnStartShowTutorial : MonoBehaviour
{
    public UnityEvent OnStartShowTutorialEvent;


    public void Start()
    {
        StartCoroutine(CoStartShowTutorial());
    }

    public IEnumerator CoStartShowTutorial()
    {
        yield return new WaitForEndOfFrame();
        OnStartShowTutorialEvent.Invoke();
    }


}
