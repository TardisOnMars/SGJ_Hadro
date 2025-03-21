using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        MUSIC,
        //AMBIANCE,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType type;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
        //print("Slider = " + volumeSlider);
    }

    private void Start()
    {
        //print("Start");
        setSliderValue();
    }

    private void setSliderValue()
    {
        switch (type)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.Instance.GetMasterLevel();
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.Instance.GetMusicLevel();
                break;
/*            case VolumeType.AMBIANCE:
                volumeSlider.value = AudioManager.Instance.GetAmbianceLevel();
                break;*/
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.Instance.GetSFXLevel();
                break;
            default:
                Debug.Log("Erreur dans les volumes.");
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (type)
        {
            case VolumeType.MASTER:
                AudioManager.Instance.SetMasterLevel(volumeSlider.value);
                break;
            case VolumeType.MUSIC:
                AudioManager.Instance.SetMusicLevel(volumeSlider.value);
                break;
/*            case VolumeType.AMBIANCE:
                AudioManager.Instance.SetAmbianceLevel(volumeSlider.value);
                break;*/
            case VolumeType.SFX:
                AudioManager.Instance.SetSFXLevel(volumeSlider.value);
                break;
            default:
                Debug.Log("Erreur dans les volumes.");
                break;
        }
    }
}
