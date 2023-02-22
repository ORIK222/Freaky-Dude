using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
[RequireComponent(typeof(Mover))]

public class Selecter : MonoBehaviour
{
    public PlayableAsset StartAnimation;
    
    public Action OnTrueAnswer;
    public Action GameWin;
    public int AnimationIndex => _animationIndex;
    
    [SerializeField] private ChoosePanel _choosePanel;
    [SerializeField] private PlayableAsset[] _chooses;

    private PlayableDirector _playableDirector;
    private int _animationIndex;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        _choosePanel = FindObjectOfType<ChoosePanel>();
    }

    private void OnEnable()
    {
        _choosePanel.AswerShowed += IncreaseIndexForChoosePanel;
    } 
    private void OnDisable()
    {
        _choosePanel.AswerShowed -= IncreaseIndexForChoosePanel;
    }

    public void BeginChoose()
    {
        _choosePanel.SetActiveTrue();
        _playableDirector.playableAsset = _chooses[0];
        _playableDirector.Play();
    }

    public void BeginSecondChoose()
    {
        _choosePanel.SetActiveTrue();
        _playableDirector.playableAsset = _chooses[_chooses.Length - 1];
        _playableDirector.Play();
    }

    public void SetPlayableAsseåt(PlayableAsset playableAsset)
    {
        _choosePanel.SetActiveTrue();
        _playableDirector.playableAsset = playableAsset;
       _playableDirector.Play();

    }



    public void CheckAnswer()
    {
        _choosePanel.SetChooseButtonNotInteractable();
        _animationIndex += _choosePanel.IsTrueAnswer ? 2 : 1;

        if (_chooses.Length - _animationIndex < 3 && _choosePanel.IsTrueAnswer)
            GameWin?.Invoke();
        
        _playableDirector.playableAsset = _chooses[_animationIndex];
        _playableDirector.Play();
    }

    public void IncreaseIndexForChoosePanel()
    {
        OnTrueAnswer?.Invoke();
    }
    public void IncreaseIndex()
    {
        _animationIndex++;
        OnTrueAnswer?.Invoke();
    }

    public void SetTimeline(PlayableAsset playableAsset)
    {
        _playableDirector.playableAsset = playableAsset;
        _playableDirector.Play();
    }

    private IEnumerator ShowTrueAnswer()
    {
        var playableAsset = _playableDirector.playableAsset;
        double delay = playableAsset.duration - (playableAsset.duration / 4.0);
        yield return new WaitForSeconds((float) delay);
        _choosePanel.SetChooseButtonNotInteractable();
    }
    public void SetCurrentState(StateData stateData)
    {
        transform.position = stateData.Position;
        _playableDirector.playableAsset = stateData.Animation;
        _animationIndex = stateData.Index;
        _playableDirector.Play();
        _choosePanel.SetNewButtonSprite((stateData.Index/2) - 1);
    }
    
}
