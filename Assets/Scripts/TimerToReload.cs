using UnityEngine;

public class TimerToReload : MonoBehaviour
{
    private float _currentTime = 5;
    private float _second;
    private bool _start;
    private void OnEnable()
    {
        _start = true;
    }

    private void FixedUpdate()
    {
        if(!_start)
            return;
        _second += Time.deltaTime;
        if (_second >= 1)
        {
            _currentTime -= 1;
            _second = 0;

        }
        if (_currentTime <= 0)
        {
            _currentTime = 10;
            LevelLoader.Instance.ReloadLevel();
        }
    }
}
