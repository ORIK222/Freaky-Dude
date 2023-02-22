using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{    
    public float FillAmount
    {
        get
        {
            return _image.fillAmount;
        }
        set
        {
            _image.fillAmount = value;
            if(_image.fillAmount > _targetProgress)
            {
                _image.fillAmount = _targetProgress;
            }
        }
    }

    [SerializeField] private float _fillSpeed = 0.5f;

    private Image _image;
    private float _targetProgress;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _targetProgress = _image.fillAmount;
    }
    private void Update()
    {
        if (FillAmount < _targetProgress)
            FillAmount += _fillSpeed * Time.deltaTime;
    }

    public void SetProgress(float progress)
    {
        _targetProgress += progress;
    }
}
