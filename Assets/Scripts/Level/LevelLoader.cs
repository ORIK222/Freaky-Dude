using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public IronSourceAdsManager AdsManager;
    public static LevelLoader Instance;
    public Action NewLevelOnLoad;
      
    private Selecter _man;
    private StateMachine _stateMachine;
    public StateData _stateData;
    private int _loseAttempt;
    private int _tempLoseAttempt;
    private bool _nextLevelLoad;

    private List<string> _eventNames;
    
    public string CurrentJasminSkin { get; set; } 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        var needSceneIndex = PlayerPrefs.GetInt("SceneIndex", 0);

        if (SceneManager.GetActiveScene().buildIndex != needSceneIndex)
            SceneManager.LoadScene(needSceneIndex);
        
        AdsManager = GetComponent<IronSourceAdsManager>();
        _stateMachine = FindObjectOfType<StateMachine>();
        _man = FindObjectOfType<Selecter>();
        Instance.NewLevelOnLoad += SetNewState;
        _loseAttempt = PlayerPrefs.GetInt("LoseAttempt", 0);
        AdsManager.InterstetialVideoEnded += ReloadLevelAfterAds;
        if (PlayerPrefs.HasKey("EventNames"))
        {
            _eventNames = JsonUtility.FromJson<List<string>>(PlayerPrefs.GetString("EventNames"));
        }
        else
        {
            _eventNames = new List<string>();
        }
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void NextLevelLoad(bool firebase = true)
    {
        _stateData = null;
        _nextLevelLoad = true;
        
        if (_eventNames.Contains("level_up_" + (SceneManager.GetActiveScene().buildIndex + 1)))
            return;
        
        if (firebase)
        {
            FirebaseAnalytics.LogEvent("level_up_" + (SceneManager.GetActiveScene().buildIndex + 1),
                new Parameter(FirebaseAnalytics.ParameterLevel, SceneManager.GetActiveScene().buildIndex + 1));
            FirebaseAnalytics.LogEvent("level_up",
                new Parameter(FirebaseAnalytics.ParameterLevel, SceneManager.GetActiveScene().buildIndex + 1));
            _eventNames.Add("level_up_" + (SceneManager.GetActiveScene().buildIndex + 1));
        }

        var nextLevelNumber = FindObjectOfType<Level>().LevelNumber + 1;
        if (nextLevelNumber > PlayerPrefs.GetInt("Record", 1))
        {
            PlayerPrefs.SetInt("Record", nextLevelNumber);
            PlayerPrefs.SetInt("SceneIndex", SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            AdsManager.ShowInterstitialVideo();
    }
    public void ReloadLevel(StateData stateData = null)
    {
        _stateData = stateData;
        if (_stateData == null)
        {
            _nextLevelLoad = false;
            _loseAttempt = PlayerPrefs.GetInt("LoseAttempt", 0);
            _loseAttempt++;
            if (_loseAttempt >= 3)
            {
                _tempLoseAttempt = 3;
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    return;
                }

                _loseAttempt = 0;
                AdsManager.ShowInterstitialVideo();
                PlayerPrefs.SetInt("LoseAttempt", _loseAttempt);
                return;
            }
            PlayerPrefs.SetInt("LoseAttempt", _loseAttempt);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NoThanks()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
        {
            _nextLevelLoad = false;
            AdsManager.ShowInterstitialVideo();
        }
    }

    public void ReloadLevelAfterAds()
    {

        if(_nextLevelLoad)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            if (_tempLoseAttempt < 3)
                return;
            _tempLoseAttempt = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }
    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(2f);
    }
    private void SetNewState()
    {
        if (_stateData == null)
            return;
        
        _stateMachine = FindObjectOfType<StateMachine>();
        _man = FindObjectOfType<Selecter>();
        _man.SetCurrentState(_stateData);
        _stateMachine.SetPreviousLevelState(_stateData);
        
        var otherObject = FindObjectOfType<WhiteTaxi>();
        if(otherObject)
            otherObject.SetStartPosition(_stateData);
        
        _stateData = null;
    }

    private void LoadCurrentLevel()
    {
        if(FindObjectsOfType<Level>().Length == 1)
            Destroy(FindObjectOfType<Level>());
        LoadLevel(PlayerPrefs.GetInt("CurrentLevel"));
    }
    
    public void SaveCurrentSkin(string skin)
    {
        CurrentJasminSkin = skin;
        PlayerPrefs.SetString("JasminSkin", skin);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("EventNames",JsonUtility.ToJson(_eventNames));
    }
}


