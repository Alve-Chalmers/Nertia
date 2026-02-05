using UnityEngine;

abstract public class PlayerForm : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfo playerInfo;
    
    protected abstract PlayerState State { get; }
    
    protected virtual void OnEnable()
    {
        playerInfo.State = State;
    }
}
