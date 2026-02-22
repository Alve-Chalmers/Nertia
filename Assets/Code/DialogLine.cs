
using System;
using UnityEngine;

[Serializable]
public class DialogLine
{
    public SOEvent eventToRaiseBefore;
    public string talkerName;
    [TextArea] public string words;
    public SOEvent eventToRaiseAfter;
}
