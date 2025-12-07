using UnityEngine;
using System.Collections.Generic;

public class BookUnlocker : MonoBehaviour
{
    public List<int> pagesToUnlock;
    public bool exclamationMark;
    private BookManager bookManager;

    void Start()
    {
        bookManager = FindFirstObjectByType<BookManager>();
        foreach(var page in pagesToUnlock)
        {
            if(exclamationMark)
                bookManager.unlockPage(page);
            else
                bookManager.noExclamationMarkUnlockPage(page);

        }

        this.gameObject.SetActive(false);
    }
}
