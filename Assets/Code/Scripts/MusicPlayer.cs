using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer Instance;

    [SerializeField] AudioSource audioSource;

    [SerializeField] SOEvent fadeOutMusic;
    [SerializeField] SOEventAudioResource tryPlayMusic;

    [SerializeField] float fadeoutTime = 3f;
    [SerializeField] float fadeoutWhenChangingTime = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        tryPlayMusic.Subscribe(OnTryPlayMusic);
        fadeOutMusic.Subscribe(OnFadeOutMusic);
    }

    void OnTryPlayMusic(AudioResource audioRes)
    {
        if (audioSource.isPlaying)
        {
            if (audioSource.resource != audioRes)
                StartCoroutine(FadeOutCurrentThenPlay(audioRes));
            return;
        }

        audioSource.resource = audioRes;
        audioSource.Play();
    }

    void OnFadeOutMusic()
    {
        if (!audioSource.isPlaying)
            return;

        StartCoroutine(FadeOut(audioSource, fadeoutTime));
    }

    private static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset for future playback
    }

    IEnumerator FadeOutCurrentThenPlay(AudioResource audioRes)
    {
        yield return StartCoroutine(FadeOut(audioSource, fadeoutWhenChangingTime));
        audioSource.resource = audioRes;
        audioSource.Play();
    }
}
