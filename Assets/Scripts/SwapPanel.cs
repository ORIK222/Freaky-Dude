using UnityEngine;

public class SwapPanel : MonoBehaviour
{
    private StateMachine _stateMachine;
    
    private void OnEnable() => _stateMachine = transform.GetComponentInParent<StateMachine>();

    public void SwapEnd() =>  _stateMachine.SwapEnd();
}
