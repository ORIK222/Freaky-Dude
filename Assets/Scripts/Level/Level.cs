using Firebase.Analytics;
using Spine.Unity.AttachmentTools;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int LevelNumber;
    public bool isComplete;
    public float CameraOffset;
    public float ProgressAmount;
    public LevelType LevelType = LevelType.Default;

    [SerializeField] private LevelLoader _levelLoader;
    private CameraMover _cameraMover;

    private void Awake()
    {
        if (FindObjectsOfType<LevelLoader>().Length == 0)
            Instantiate(_levelLoader);
    }
    private void Start()
    {
        _cameraMover = FindObjectOfType<CameraMover>();
        _cameraMover.SetNewLevel(this);
        PlayerPrefs.SetInt("CurrentLevel", LevelNumber);
        LevelLoader.Instance.NewLevelOnLoad?.Invoke();
        if(LevelNumber == 31)
            FirebaseAnalytics.LogEvent("last_level_complete", new Parameter(FirebaseAnalytics.ParameterLevel, 31));
        AtlasUtilities.ClearCache();
        Resources.UnloadUnusedAssets();
     
    }
}

public enum LevelType
{
    Default,
    CutScene
}

