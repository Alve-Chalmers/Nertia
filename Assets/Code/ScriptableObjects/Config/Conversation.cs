using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/Config/Conversation")]
public class Conversation : ScriptableObject
{
    [NonSerialized] public bool hasBeen = false;
    public bool canHaveAgain = false;
    [SerializeField] public List<DialogLine> dialogLines;
}
