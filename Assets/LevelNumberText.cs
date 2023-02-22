using TMPro;
using UnityEngine;

public class LevelNumberText : MonoBehaviour
{
    [SerializeField] private bool _isCurrentLevel;
    
    private TextMeshProUGUI _text;
    private Level _level;
    private void OnEnable()
    {
        if(!_text)
            _text = FindObjectOfType<TextMeshProUGUI>();
        if (!_level)
            _level = FindObjectOfType<Level>();
        
        var nextLevelNumber = _level.LevelNumber + 1;
        _text.text = _isCurrentLevel ?_level.LevelNumber.ToString() : nextLevelNumber.ToString();
    }
}
