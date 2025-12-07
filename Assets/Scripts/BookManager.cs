using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BookManager : MonoBehaviour
{
    public GameObject[] pagesList;
    public GameObject emptyPage;
    private int currentPage;
    private bool[] unlockedPages;
    public GameObject rightArrow;
    public GameObject leftArrow;

    public UnityEvent onUnlock;

    void Start()
    {
        unlockedPages = Enumerable.Repeat<bool>(false, pagesList.Length).ToArray();
        currentPage = 0;
        pagesList[currentPage].SetActive(true);
        leftArrow.SetActive(false);
    }

    public void unlockPage(int page)
    {
        if (unlockedPages[page]) return;

        unlockedPages[page] = true;
        onUnlock.Invoke();
    }

    public void noExclamationMarkUnlockPage(int page)
    {
        unlockedPages[page] = true;
    }

    public void nextPage()
    {
        AudioManager.Instance.PlaySoundOneShoot("CarnetChangement");
        if(unlockedPages[currentPage + 1])
        {
            pagesList[currentPage].SetActive(false);
            currentPage++;
            pagesList[currentPage].SetActive(true);
        }
        else
        {
            pagesList[currentPage].SetActive(false);
            emptyPage.SetActive(true);
            currentPage++;
        }
        if (currentPage >= pagesList.Length-1)
        {
            rightArrow.SetActive(false);
        }
        leftArrow.SetActive(true);
    }

    public void previousPage()
    {
        AudioManager.Instance.PlaySoundOneShoot("CarnetChangement");
        if (unlockedPages[currentPage - 1])
        {
            pagesList[currentPage].SetActive(false);
            currentPage--;
            pagesList[currentPage].SetActive(true);
            emptyPage.SetActive(false);
        }
        else
        {
            pagesList[currentPage].SetActive(false);
            emptyPage.SetActive(true);
            currentPage--;
        }
        if (currentPage <= 0)
        {
            leftArrow.SetActive(false);
        }
        rightArrow.SetActive(true);
    }

    public void PlayOpenSound()
    {
        AudioManager.Instance.PlaySoundOneShoot("CarnetOuverture");
    }
    
    public void PlayCloseSound()
    {
        AudioManager.Instance.PlaySoundOneShoot("CarnetFermeture");
    }

    public void OnEnable()
    {
        currentPage = 0;
        pagesList[currentPage].SetActive(true);
        rightArrow.SetActive(true);
    }

    public void OnDisable()
    {
        pagesList[currentPage].SetActive(false);
        emptyPage.SetActive(false);
        leftArrow.SetActive(false);
    }
}
