using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private RectTransform _prewiewPanel;
    [SerializeField] private RectTransform _losePanel;
    private StateMachine _stateMachine;

    private StateData _stateData;
    private int _loseAttempt;

    private void OnEnable()
    {
        if (!_stateMachine)
            _stateMachine = FindObjectOfType<StateMachine>();
        LevelLoader.Instance.AdsManager.RewardedVideoEnded += LoadPreviousState;
    } 
    private void OnDisable()
    {
        if (!_stateMachine)
            _stateMachine = FindObjectOfType<StateMachine>();
        LevelLoader.Instance.AdsManager.RewardedVideoEnded -= LoadPreviousState;
    }

    public void ShowVideo(StateData stateData)
    {
        _stateData = stateData;
        LevelLoader.Instance.AdsManager.ShowRewardedVideo();
    }

    private void LoadPreviousState()
    {
        _stateMachine.LoadPreviousState(_stateData);
        gameObject.SetActive(false);
    }

    public void PrewiewEnd()
    {
        _prewiewPanel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(true);
    }

    public void NoThanksButtonOnClick()
    {
        LevelLoader.Instance.NoThanks();
    }
}
