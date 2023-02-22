using UnityEngine;

public class AtmSound : MonoBehaviour
{
    [SerializeField] private AudioClip _atmBackgroundSound;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Play();
    }
    
    public void Play()
    {
        _audioSource.clip = _atmBackgroundSound;
        _audioSource.Play();
    }

    public void SetBackGroundMusic(AudioClip clip)
    {
        _atmBackgroundSound = clip;
        Play();
    }
    
}
