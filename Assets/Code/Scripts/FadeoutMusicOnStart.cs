using UnityEngine;

public class FadeoutMusicOnStart : MonoBehaviour
{
    [SerializeField] SOEvent fadeoutMusic;

    void Start()
    {
        fadeoutMusic.Raise();
    }
}
