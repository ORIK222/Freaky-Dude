using UnityEngine;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
    public bool IsChoose { get; set; }
    public bool IsTrueAnswer { get; set; }

    [SerializeField] private ImageType _currentButtonSprite;
    [SerializeField] private Sprite _currentAnswerResultSprite;
    [SerializeField] private ImageType[] _newTypeButtonSprite;
    [SerializeField] private Sprite[] _newTypeAnswerResultSprite;

    private int _index = 0;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        IsChoose = false;
        _button.image.sprite = _currentButtonSprite.Sprite;
        IsTrueAnswer = _currentButtonSprite.IsTrue;
    }

    public void SetAnswerSprite()
    {
        _button.image.sprite = _currentAnswerResultSprite;
    }
    public void SetNewAnswerSprite(int index)
    {
        _button.image.sprite = _newTypeButtonSprite[index].Sprite;
        _currentAnswerResultSprite = _newTypeAnswerResultSprite[index];
        IsChoose = false;
        IsTrueAnswer = _newTypeButtonSprite[index].IsTrue;
    }

    public void Choose()
    {
        IsChoose = true;
    }
}
