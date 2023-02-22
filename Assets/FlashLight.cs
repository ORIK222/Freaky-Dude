using Spine.Unity;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SpineAnimation(), SerializeField] private string _off;
    [SpineAnimation(), SerializeField] private string _on;
    [SerializeField] private SkeletonGraphic _skeletonGraphic;
    
    private void OnEnable()
    {
        _skeletonGraphic.AnimationState.SetAnimation(0, _on, false);
    }

    private void OnDisable()
    {
        _skeletonGraphic.AnimationState.SetAnimation(0, _off, false);
    }
}
