
using System;
using UnityEngine;

[Serializable]
public class DialogLine
{
    public SOEvent eventToRaiseBefore;
    public bool hideSpacebarPrompt = false;
    public string talkerName;
    [TextArea] public string words;
    public SOEvent eventToRaiseAfter;
}
