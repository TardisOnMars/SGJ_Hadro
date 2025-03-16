using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => _instance;

    private static AudioManager _instance;

    public List<AudioClip> audioClips;
    public AudioSource oneShootSource;
    public AudioSource ambientMusicSource;
    public AudioClip ambientMusic;

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


}
