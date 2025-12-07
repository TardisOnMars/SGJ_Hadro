using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI _textEgg;
    public TextMeshProUGUI _textYounglings;
    public TextMeshProUGUI _textHadro;
    public TextMeshProUGUI _textCurrentTotal;
    public TextMeshProUGUI _textVictory;
    public GameObject _exclamationMark;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textVictory.text = GameManager.Instance.victoryAmount.ToString();
        
        GameManager.Instance.addEgg.AddListener(UpdateEggUI);
        GameManager.Instance.killEgg.AddListener(UpdateEggUI);
        GameManager.Instance.addYoungling.AddListener(UpdateYounglingsUI);
        GameManager.Instance.killYoungling.AddListener(UpdateYounglingsUI);
        GameManager.Instance.addHadro.AddListener(UpdateHadroUI);
        GameManager.Instance.killHadro.AddListener(UpdateHadroUI);
        GameManager.Instance.checkPopulation.AddListener(UpdatePopulationUI);

        UpdatePopulationUI();
        UpdateEggUI();
        UpdateYounglingsUI();
        UpdateHadroUI();
    }

    public void ToggleExclamationMark(bool status)
    {
        _exclamationMark.SetActive(status);
    }

    private void UpdatePopulationUI()
    {
        _textCurrentTotal.text = GameManager.Instance.individualTotalCount.ToString();
    }

    private void UpdateHadroUI()
    {
        _textHadro.text = GameManager.Instance.hadroCount.ToString();
    }

    private void UpdateYounglingsUI()
    {
        _textYounglings.text = GameManager.Instance.younglingCount.ToString();
    }

    private void UpdateEggUI()
    {
        _textEgg.text = GameManager.Instance.eggCount.ToString();
    }

    
}
