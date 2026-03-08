using UnityEngine;

[CreateAssetMenu(fileName = "PlaythroughStats", menuName = "ScriptableObjects/Data/PlaythroughStats")]
public class PlaythroughStats : ScriptableObject
{
    [System.NonSerialized, ShowInPlayMode] public float time;
    [System.NonSerialized, ShowInPlayMode] public int deaths;
}
