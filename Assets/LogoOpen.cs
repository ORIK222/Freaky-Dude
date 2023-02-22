using UnityEngine;

public class LogoOpen : MonoBehaviour
{
    private int _temp;
    private void OnEnable()
    {
        _temp = PlayerPrefs.GetInt("GameOpen", 0);
        if (_temp == 1)
            transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("GameOpen", 1);
    }

    private void OnApplicationPause()
    {
        PlayerPrefs.SetInt("GameOpen", 0);
    }
}
