using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

public class StartAnimation : MonoBehaviour
{
    public UnityAction OnThief;

    [SerializeField] private AnimationReferenceAsset _false, _true;

    private Spine.AnimationState _animationState;
    private SkeletonAnimation _skeletonAnimation;
    private string _currentState;
    private string _currentAnimation;

    private void Start()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _animationState = _skeletonAnimation.AnimationState;
        _currentState = "true";
        SetCharacterState(_currentState);
        _animationState.Complete += OnSpineAnimationComplete;
    }

    public void OnSpineAnimationComplete(TrackEntry trackEntry)
    {
        if(trackEntry.Animation.Name == "NLO")
            OnThief?.Invoke();
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("true"))
            SetAnimation(_true, true, 1f);
        else
            SetAnimation(_false, false, 1f);

        _currentState = state;
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(_currentAnimation))
            return;

        TrackEntry animationEntry = _skeletonAnimation.state.SetAnimation(0, animation, loop);

        animationEntry.TimeScale = timeScale;
        _currentAnimation = animation.name;
    }
}
