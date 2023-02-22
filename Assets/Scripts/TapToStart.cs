using System;
using UnityEngine;

public class TapToStart : MonoBehaviour
{
    public Action TapToStartOnClick;
    
    public void Click()
    {
        TapToStartOnClick?.Invoke();
    }
}
