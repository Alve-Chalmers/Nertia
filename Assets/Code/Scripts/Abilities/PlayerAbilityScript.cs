using UnityEngine;

abstract public class PlayerAbilityScript : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfo playerInfo;
    protected abstract PlayerAbilityType Ability { get; }
    
    protected virtual void OnEnable()
    {
        if (playerInfo != null)
        {
            playerInfo.AbilityUsed = Ability;
        }
    }
}
