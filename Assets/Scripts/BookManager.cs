using System.Linq;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public GameObject[] pagesList;
    public GameObject emptyPage;
    private int currentPage;
    private bool[] unlockedPages;
    public GameObject rightArrow;
    public GameObject leftArrow;
    void Start()
    {
        unlockedPages = Enumerable.Repeat<bool>(true, pagesList.Length-1).ToArray();
        unlockedPages[0] = true;
        unlockedPages[1] = true;
        unlockedPages[2] = true;
        currentPage = 0;
        pagesList[currentPage].SetActive(true);
        leftArrow.SetActive(false);
    }

    public void unlockPage(int page)
    {
        unlockedPages[page] = true;
    }

    public void nextPage()
    {
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
        if (currentPage >= pagesList.Length-2)
        {
            rightArrow.SetActive(false);
        }
        leftArrow.SetActive(true);
    }

    public void previousPage()
    {
        if (unlockedPages[currentPage - 1])
        {
            pagesList[currentPage].SetActive(false);
            currentPage--;
            pagesList[currentPage].SetActive(true);
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
}
