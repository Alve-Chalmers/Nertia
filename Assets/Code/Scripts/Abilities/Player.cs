using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInfo pi;
    [SerializeField] Rigidbody2D prb;
    [SerializeField] SOEvent outsideCameraEvent;

    [SerializeField] AudioSource windAudio;

    void Awake()
    {
        pi.Position = transform.position;
        pi.Velocity = prb.linearVelocity;
    }

    void OnEnable()
    {
        outsideCameraEvent.Subscribe(OnPlayerDeathRequested);
    }
    void OnDisable()
    {
        outsideCameraEvent.Unsubscribe(OnPlayerDeathRequested);
    }

    void Update()
    {
        pi.Position = transform.position;
        pi.Velocity = prb.linearVelocity;

        Debug.Log(pi.Velocity.magnitude);

        UpdateWindAudio();
    }

    void UpdateWindAudio()
    {
        float targetVolume = Mathf.SmoothStep(0f, 1f, (pi.Velocity.magnitude - 30f) / 30f);
        float targetPitch = Mathf.SmoothStep(0.7f, 1.3f, (pi.Velocity.magnitude - 30f) / 30f);

        if (pi.IsGrounded)
        {
            targetVolume = 0;
        }

        windAudio.volume = Mathf.MoveTowards(windAudio.volume, targetVolume, Time.deltaTime*0.5f);
        windAudio.pitch = Mathf.MoveTowards(windAudio.pitch, targetPitch, Time.deltaTime*0.5f);

    }

    void OnPlayerDeathRequested() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
