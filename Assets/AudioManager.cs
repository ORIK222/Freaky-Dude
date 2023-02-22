using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _gameOver;
    [SerializeField] private AudioClip _timeLose;
    [SerializeField] private AudioClip _win; 
    [SerializeField] private AudioClip _backgroundMusic; 

    private AudioSource _audioSource;
    private Selecter _man;
    
    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
        FindMan();
    }

    private void OnEnable()
    {
        GetComponentInParent<LevelLoader>().NewLevelOnLoad += FindMan;
        _man.GameWin += Stop;
    }

    private void Stop()
    {
        _audioSource.Stop();
    }

    private void OnDisable()
    {
        LevelLoader.Instance.NewLevelOnLoad -= FindMan;
    }
    
    

    private void FindMan()
    {
        _audioSource.clip = _backgroundMusic;
        _audioSource.Play();
        _man = FindObjectOfType<Selecter>();
    }
    

}
