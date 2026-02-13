using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StartingAbilities", menuName = "ScriptableObjects/Config/StartingAbilities")]
public class StartingAbilities : ScriptableObject
{
    public List<PlayerAbilityType> StartingWith;
}
