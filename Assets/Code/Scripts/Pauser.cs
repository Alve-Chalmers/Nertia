using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pauser : MonoBehaviour
{
    [SerializeField] SOEventBool setPause;
    [SerializeField] float fadeDuration = 0.3f;

    InputAction pauseAction;
    bool isPaused;
    Coroutine fadeCoroutine;

    void OnEnable()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        pauseAction.performed += OnPauseInput;
    }

    void OnDisable()
    {
        pauseAction.performed -= OnPauseInput;
    }

    void OnPauseInput(InputAction.CallbackContext _)
    {
        isPaused = !isPaused;
        setPause.Raise(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (isPaused)
            fadeCoroutine = StartCoroutine(FadeOutAudio());
        else
            fadeCoroutine = StartCoroutine(FadeInAudio());
    }

    IEnumerator FadeOutAudio()
    {
        float start = AudioListener.volume;
        for (float t = 0f; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            AudioListener.volume = Mathf.Lerp(start, 0f, t / fadeDuration);
            yield return null;
        }
        AudioListener.volume = 0f;
        AudioListener.pause = true;
    }

    IEnumerator FadeInAudio()
    {
        AudioListener.pause = false;
        float start = AudioListener.volume;
        for (float t = 0f; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            AudioListener.volume = Mathf.Lerp(start, 1f, t / fadeDuration);
            yield return null;
        }
        AudioListener.volume = 1f;
    }
}
