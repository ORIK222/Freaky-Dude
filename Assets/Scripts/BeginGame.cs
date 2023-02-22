using UnityEngine;
using UnityEngine.UI;

public class BeginGame : MonoBehaviour
{
    [SerializeField] private GameObject _tapToStartImage;
    [SerializeField] private StartAnimation _character;
    [SerializeField] private GameObject _swapPanel;

    private void OnEnable()
    {
        _character.OnThief += Swap;
    }
    private void OnDisable()
    {
        _character.OnThief -= Swap;
    }
    private void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            _tapToStartImage.gameObject.SetActive(false);
            _character.SetCharacterState("false");
        }    
    }

    private void Swap()
    {
        _swapPanel.SetActive(true);
    }    
}
