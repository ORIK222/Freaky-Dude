
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePanel : MonoBehaviour
{
    public bool IsTrueAnswer { get; set; }

    [SerializeField] private ChooseButton _trueAnswerButton;
    [SerializeField] private ChooseButton _falseAnswerButton;

	private ChooseButton _needButton;
    public Action AswerShowed;
    
    private void Awake()
    {
        _falseAnswerButton.GetComponent<Button>().interactable = false;
        _trueAnswerButton.GetComponent<Button>().interactable = false;
    }
    public void SetChooseButtonNotInteractable()
    {
        _trueAnswerButton.GetComponent<Button>().interactable = false;
        _falseAnswerButton.GetComponent<Button>().interactable = false;
		_needButton = _trueAnswerButton.IsChoose ? _trueAnswerButton : _falseAnswerButton;
        IsTrueAnswer = _needButton.IsTrueAnswer;
    }

    public void SetAnswerResult()
    {
        _needButton.SetAnswerSprite();
        if(IsTrueAnswer)
            AswerShowed?.Invoke();
    }
    
    public void SetNewButtonSprite(int index)
    {
        _trueAnswerButton.SetNewAnswerSprite(index);
        _falseAnswerButton.SetNewAnswerSprite(index);
        _falseAnswerButton.GetComponent<Button>().interactable = true;
        _trueAnswerButton.GetComponent<Button>().interactable = true;
        DisabledThisPanel();
    }

    public void DisabledThisPanel()
    {
        var color = _trueAnswerButton.GetComponent<Button>().image.color;
        color.a = 0f;
        _trueAnswerButton.GetComponent<Button>().image.color = color;
        _falseAnswerButton.GetComponent<Button>().image.color = color;
        _falseAnswerButton.GetComponent<Button>().interactable = false;
        _trueAnswerButton.GetComponent<Button>().interactable = false;
    }
    public void SetActiveTrue()
    {
        var color = _trueAnswerButton.GetComponent<Button>().image.color;
        color.a = 255f;
        _trueAnswerButton.GetComponent<Button>().image.color = color;
        _falseAnswerButton.GetComponent<Button>().image.color = color;
        _falseAnswerButton.GetComponent<Button>().interactable = true;
        _trueAnswerButton.GetComponent<Button>().interactable = true;
    }
}
