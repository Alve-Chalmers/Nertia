using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListOfAbilities", menuName = "ScriptableObjects/Config/ListOfAbilities")]
public class ListOfAbilities : ScriptableObject
{
    public List<PlayerAbilityType> abilities;
}
