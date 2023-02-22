using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform Target;
    
    [SerializeField] private float _speed;
    [SerializeField] private bool _canMove;

    private void Start()
    {
        if (!Target)
            _canMove = false;
    }
    
    private void FixedUpdate()
    {
        if (_canMove)
            MoveOn();
    }

    public void BeginMove()
    {
        transform.position = new Vector3(transform.position.x, -1.85f, transform.position.z);
    }
    public void SetTarget(Transform target)
    {
        Target = target;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void SetParent(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = new Vector3(3, 32, -1);
    }
    public void CanMove()
    {
        _canMove = true;
    }

    public void UnCanMove()
    {
        _canMove = false;
    }
    private void MoveOn()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, _speed * Time.deltaTime);
        if (transform.position == Target.position)
            _canMove = false;
    }

}
