using UnityEngine;

public class WhiteTaxi : MonoBehaviour
{
    public void SetStartPosition(StateData stateData)
    {
        transform.position = stateData.OtherObjectPosition[0];
    }
}
