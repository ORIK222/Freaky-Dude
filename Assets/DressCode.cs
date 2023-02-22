using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressCode : MonoBehaviour
{
    [SerializeField] private int _currentIndex = 4;
    [SerializeField] private bool _needCheckAnimation;

    private AnimationEvents _jasmine;
    private string _skinName;


    private void Awake()
    {
        _jasmine = FindObjectOfType<AnimationEvents>();
        _skinName = PlayerPrefs.GetString("JasmineSkin", "jeans");
    }
    private void Start()
    {
        var animationIndex = LevelLoader.Instance._stateData.Index;
        if (animationIndex >= _currentIndex) 
            _jasmine.SetSkin(_skinName);

    }

    private void SetNeedPlayableDirector(int animationIndex)
    {
        print("AnimationIndex: " + animationIndex);
        print("CurrentSkin: " + _skinName);
        if (animationIndex != 3)
            return;
        if (_skinName == "jeans")
            LevelLoader.Instance._stateData.Index = 2;
        else
            LevelLoader.Instance._stateData.Index = 1;
    }
}
