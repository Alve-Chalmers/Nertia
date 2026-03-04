using UnityEngine;
using UnityEngine.Audio;

public class PlayMusicWhenPlayerEnter : MonoBehaviour
{
    [SerializeField] AudioResource audioRes;
    [SerializeField] SOEventAudioResource tryPlay;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (tryPlay == null || audioRes == null)
            return;


        if (!col.gameObject.CompareTag("Player"))
            return;

        tryPlay.Raise(audioRes);
    }
}
