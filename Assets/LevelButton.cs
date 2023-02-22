using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int _offset;
    [SerializeField] private int _panelCount;
    [SerializeField] private Button _leftSwipeButton;
    [SerializeField] private Button _rightSwipeButton;
    
    private int _currentPanelNumber = 1;
    private SelectButton[] _selectButtons;
    private Level _currentLevel;
    
    private RectTransform _rectTransform;
    
    private void OnEnable()
    {
        _currentLevel = FindObjectOfType<Level>();
        _rectTransform = GetComponent<RectTransform>();
        CheckLevelState();
    }
    public void LoadLevel(int levelNumber)
    {
       LevelLoader.Instance.LoadLevel(levelNumber);
       PlayerPrefs.SetInt("CurrentLevel",levelNumber);
    }


    private void InitSelectButton()
    {
        _selectButtons = new SelectButton[transform.childCount];
        for (int i = 0; i < _selectButtons.Length; i++)
        {
            _selectButtons[i] = transform.GetChild(i).GetComponent<SelectButton>();
            _selectButtons[i].Init();
        }
    }
    public void CheckLevelState()
    {
        InitSelectButton();
        var levelCount = PlayerPrefs.GetInt("Record", 1);
        levelCount = levelCount > 30 ? 30 : levelCount;
        var currentLevelNumber = _currentLevel.LevelNumber;
        
        for (int i = 0; i < _selectButtons.Length; i++)
        {
            if (i > levelCount - 1)
                _selectButtons[i].Lock();
            else if (i == currentLevelNumber - 1)
                _selectButtons[i].CurrentLevelSetSprite();
            else
                _selectButtons[i].Unlock();
        }
    }

    public void SwipeRight()
    {
        var anchoredPosition = _rectTransform.anchoredPosition;
        
        anchoredPosition = new Vector3(
              anchoredPosition.x - _offset, 
                anchoredPosition.y, 
                _rectTransform.position.z);
        
        _rectTransform.anchoredPosition = anchoredPosition;
        _rightSwipeButton.interactable = ++_currentPanelNumber != _panelCount;
        _leftSwipeButton.interactable = true;
    }
    public void SwipeLeft()
    {
        var anchoredPosition = _rectTransform.anchoredPosition;
        anchoredPosition = new Vector3(
            anchoredPosition.x + _offset, 
            anchoredPosition.y, 
            _rectTransform.position.z);
        _rectTransform.anchoredPosition = anchoredPosition;

        _leftSwipeButton.interactable = --_currentPanelNumber != 1;
        _rightSwipeButton.interactable = true;
    }
}
