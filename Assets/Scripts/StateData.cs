using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "State", menuName = "State/LevelStateData", order = 1)]
public class StateData : ScriptableObject
{
    public Vector3 Position;
    public Vector3 TargetPosition;
    public PlayableAsset Animation;
    public float Progress;
    public int Index;

    public Vector3[] OtherObjectPosition;
}
