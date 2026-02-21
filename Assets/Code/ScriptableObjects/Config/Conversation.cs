using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/Config/Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] public List<DialogLine> dialogLines;
}
