using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public void NextLevelLoad()
    {
        LevelLoader.Instance.NextLevelLoad();
    }
}
