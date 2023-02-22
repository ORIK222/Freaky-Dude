using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Transform _levelsPanel;
    [SerializeField] private LevelButton _levelButton;
    public void LevelButtonOnClick()
    {
        _levelsPanel.gameObject.SetActive(!_levelsPanel.gameObject.activeInHierarchy);
    }

    public void OpenLevelButton()
    {
        _levelButton.CheckLevelState();
    }
}
