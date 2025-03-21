using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => _instance;

    private static AudioManager _instance;

    public List<AudioClip> audioClips;
    public AudioSource oneShootSource;
    public AudioSource ambientMusicSource;
    public AudioClip ambientMusic;


    [Header("Volume")]
    [Range(0, 1)]
    private float masterVolume = 0.75f;
    [Range(0, 1)]
    private float musicVolume = 0.75f;
    [Range(0, 1)]
    private float ambianceVolume = 0.75f;
    [Range(0, 1)]
    private float SFXVolume = 0.75f;

    public AudioMixer mixer;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        // Je ne sais jamais si il faut mettre ca ici ou dans le "Awake"
        SetMasterLevel(PlayerPrefs.GetFloat("MasterVolume", 0.75f));
        SetSFXLevel(PlayerPrefs.GetFloat("SFXVolume", 0.75f));
        SetMusicLevel(PlayerPrefs.GetFloat("MusicVolume", 0.75f));

        ambientMusicSource.clip = ambientMusic;
        ambientMusicSource.Play();
    }

    public void PlaySoundOneShoot(string clipName)
    {
        AudioClip clip = audioClips.Find(audio => audio.name.Equals(clipName));
        if (clip != null)
        {
            oneShootSource.PlayOneShot(clip);
        }
    }

    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
        masterVolume = sliderValue;
    }

    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        SFXVolume = sliderValue;
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        musicVolume = sliderValue;
    }

    public float GetMasterLevel()
    {
        //Debug.Log("AM - GetMasterLevel");
        return masterVolume;
    }
    public float GetMusicLevel()
    {
        //Debug.Log("AM - GetMusicLevel");
        return musicVolume;
    }
    public float GetSFXLevel()
    {
        //Debug.Log("AM - GetSFXLevel");
        return SFXVolume;
    }
}
