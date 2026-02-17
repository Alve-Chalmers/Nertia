using UnityEngine;

abstract public class PlayerAbilityScript : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfo playerInfo;
    protected abstract PlayerAbilityType Ability { get; }
    
    protected virtual void OnEnable()
    {
        playerInfo.AbilityUsed = Ability;
    }
}
