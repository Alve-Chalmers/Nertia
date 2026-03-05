using UnityEngine;
using UnityEngine.Audio;

public class PlayMusicWhenPlayerEnter : MonoBehaviour
{
    [SerializeField] AudioResource audioRes;
    [SerializeField] bool loop;
    [SerializeField] SOEventAudioResource tryPlay;
    [SerializeField] SOEventBool setMusicLooping;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (tryPlay == null || audioRes == null)
            return;


        if (!col.gameObject.CompareTag("Player"))
            return;

        setMusicLooping.Raise(loop);
        tryPlay.Raise(audioRes);
    }
}
