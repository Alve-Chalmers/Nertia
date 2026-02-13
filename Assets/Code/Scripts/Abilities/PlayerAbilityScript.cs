using UnityEngine;

abstract public class PlayerAbilityScript : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfo playerInfo;
    protected abstract PlayerAbilityType State { get; }
    
    protected virtual void OnEnable()
    {
        playerInfo.State = State;
    }
}
