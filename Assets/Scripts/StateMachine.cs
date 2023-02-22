using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public StateData CurrentStateData { get; private set; }
    
    [SerializeField] private TapToStart _tapToStart;
    [SerializeField] private MenuController _menu;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private SwapPanel _swapPanel;
    [SerializeField] private WinPanel _winPanel;
    
    private Selecter _man;
    private LevelLoader _levelLoader;
    private Level _level;
    public bool SwapPanelEnabled => _swapPanel.enabled;
    private void OnEnable()
    {
        _level = FindObjectOfType<Level>();
        _levelLoader = FindObjectOfType<LevelLoader>();
        _tapToStart.TapToStartOnClick += TapToStartClick;
        _man = FindObjectOfType<Selecter>();
        _man.OnTrueAnswer += IncreaseProgress;
        _man.GameWin += LevelWin;
    }

    public void LoadPreviousState(StateData stateData)
    {
        _levelLoader.ReloadLevel(stateData);
    }
    private void TapToStartClick()
    {
        if (!_menu.enabled)
            return;
        
        _progressBar.transform.parent.gameObject.SetActive(true);
        _menu.gameObject.SetActive(false);
        _man.SetTimeline(_man.StartAnimation);
    }

    private void OnDisable()
    {
        _tapToStart.TapToStartOnClick -= TapToStartClick;
    }
    public void SwapEnd()
    {
        if (_level.LevelType == LevelType.CutScene)
        {
            _levelLoader.NextLevelLoad(false);
            return;
        }
        
        if (_level.isComplete)
        {
            _swapPanel.gameObject.SetActive(false);
            _winPanel.gameObject.SetActive(true);
        }
        else
            _levelLoader.ReloadLevel();
    }
    private void IncreaseProgress()
    {
        _progressBar.SetProgress(_level.ProgressAmount);
    }
    private void LevelWin()
    {
        _level.isComplete = true;
        
        if(_level.LevelNumber > PlayerPrefs.GetInt("LevelNumber"))
            PlayerPrefs.SetInt("LevelNumber", _level.LevelNumber);
    }
    
    public void SetPreviousLevelState(StateData stateData)
    {
        _progressBar.transform.parent.gameObject.SetActive(true);
        _progressBar.SetProgress(stateData.Progress);
        CurrentStateData = stateData;
    }
    
}
