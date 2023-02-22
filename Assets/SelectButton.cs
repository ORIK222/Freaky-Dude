using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public int Number { get; set; }

    [SerializeField] private Sprite _currentLevelSprite;
    [SerializeField] private Sprite _closedLevelSprite;

    private TMP_Text _text;
    private Button _button;
    private Sprite _defaulSprite;

    public void Init()
    {
        _button = GetComponent<Button>();
        _text = transform.GetChild(0).GetComponent<TMP_Text>();
        _defaulSprite = _button.image.sprite;
        _button.image.sprite = _closedLevelSprite;
        Number = int.Parse(_text.text);
    }

    public void Lock()
    {
        _button.image.sprite = _closedLevelSprite;
        _text.gameObject.SetActive(false);
        _button.interactable = false;
    }
    public void Unlock()
    {
        _button.image.sprite = _defaulSprite;
        _button.interactable = true;
        _text.gameObject.SetActive(true);
    }
    public void CurrentLevelSetSprite()
    {
        _button.image.sprite = _currentLevelSprite;
    }
}
