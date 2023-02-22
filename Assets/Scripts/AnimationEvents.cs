using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class AnimationEvents : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;
    private PlayableDirector _playableDirector;
    private CameraMover _camera;
    private bool _isStop;

    private void Awake()
    {
        _camera = FindObjectOfType<CameraMover>();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _playableDirector = GetComponent<PlayableDirector>();
    }
    private void Start()
    {
        _playableDirector.Play();
    }

    public void ObjectEnabled(GameObject other)
    {
        other.gameObject.SetActive(true);
    }
    public void ObjectDestroy(GameObject other)
    {
        other.SetActive(false); 
    }
    public void StopAnimation()
    {
        _playableDirector.Pause();
    }
    public void FlipX()
    {
        _skeletonAnimation.Skeleton.ScaleX = -_skeletonAnimation.Skeleton.ScaleX;
    }
    public void FlipObjectX(SkeletonAnimation skeletonAnimation)
    {
        skeletonAnimation.Skeleton.ScaleX = -skeletonAnimation.Skeleton.ScaleX;      
    }
    public void SetStop()
    {
        _isStop = true;
    }
    public void StopAnimationWithCondition()
    {
        if (_isStop)
            _playableDirector.Pause();
    }   
    public void SetVisible()
    {
        var position = transform.position;
        position.z = 10;
        transform.position = position;
    }

    public void SetCameraOffset(float offset)
    {
        _camera.SetOffset(offset);
    }
    
    public void SetPositionZ(Transform _target)
    {
        var position = _target.position;
       position.z = -4;
       _target.position = position;
    }
    
    public void SetZPosition(float z)
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y, z);
    }
    
    public void SetYPosition(float y)
    {
        transform.position = new Vector3 (transform.position.x, y, transform.position.z);
    }

    public void DeleteCameraTarget()
    {
        _camera.DeleteTarget();
    }
    
    public void SetSkin(string name)
    {
        _skeletonAnimation.skeleton.SetSkin(name);
        _skeletonAnimation.skeleton.SetToSetupPose();
        _skeletonAnimation.AnimationState.Apply(_skeletonAnimation.skeleton);
    }
	public void SetSkinForJasmin(SkeletonAnimation jasmine)
    {
        if (jasmine == null)
            return;
		
        jasmine.skeleton.SetSkin("Blue_Dress");
        jasmine.skeleton.SetToSetupPose();
        jasmine.AnimationState.Apply(jasmine.skeleton);
    }

    public void SetSkinOnReload(SkeletonAnimation skeletonAnimation)
    {
        if (string.IsNullOrEmpty(LevelLoader.Instance.CurrentJasminSkin))
            return;
        
        skeletonAnimation.Skeleton.SetSkin(LevelLoader.Instance.CurrentJasminSkin);
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
        skeletonAnimation.Skeleton.Update(0); 
    }
    
    public void SetSkinOnReload(SkeletonGraphic skeletonAnimation)
    {
        var skin = PlayerPrefs.GetString("JasminSkin");
        
        if (string.IsNullOrEmpty(skin))
            return;
        
        skeletonAnimation.Skeleton.SetSkin(skin);
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
        skeletonAnimation.Skeleton.Update(0); 
    }
    
    public void ChangeCurrentSkin(string skin)
    {
        PlayerPrefs.SetString("JasmineSkin", skin);
    }
}
