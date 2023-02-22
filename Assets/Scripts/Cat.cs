using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private bool _canMove;
    private void FixedUpdate()
    {
        if (_canMove)
            transform.position = _target.position;
    }

    public void CanMove()
    {
        _canMove = true;
    }
}
