using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Transform _target;
    private Level _currentLevel;
    
    private float _smoothTime = 0.3f;
    private float _offset;

    Vector3 _tempPosition;
    private void Start()
    {
        if(!_target)
            _target = FindObjectOfType<Selecter>().transform;
        _tempPosition = transform.position;
        _offset = FindObjectOfType<Level>().CameraOffset;
    }
    
    private void FixedUpdate()
    {
        if (_target == null) 
            return;

        var position = transform.position;
        _tempPosition.x = Mathf.Lerp(position.x, _target.position.x, _smoothTime);

        _tempPosition.z = -15;
        _tempPosition.x += _offset;
        position = _tempPosition;
        transform.position = position;
    }

    public void SetNewLevel(Level level)
    {
        _target = FindObjectOfType<CameraTarget>().transform;
        _currentLevel = level;
        _offset = _currentLevel.CameraOffset;
    }

    public void SetTarget(Transform target)
    {
        _target = target; 
    }

    public void DeleteTarget()
    {
        _target = null;
    }
    public void SetOffset(float offset)
    {
        _offset = offset;
    }
}

