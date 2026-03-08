using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInfo pi;
    [SerializeField] Rigidbody2D prb;
    [SerializeField] SOEvent playerShouldDie;

    [SerializeField] AudioSource windAudio;
    [SerializeField] SOEvent freezePlayerInPlace;
    [SerializeField] SOEventString gotoScene;
    [SerializeField] PlaythroughStats playthroughStats;
    [SerializeField] Animator playerAnimator;

    void Awake()
    {
        pi.Position = transform.position;
        pi.Velocity = prb.linearVelocity;
    }

    void OnEnable()
    {
        playerShouldDie.Subscribe(OnPlayerDeathRequested);
        freezePlayerInPlace.Subscribe(OnFreezePlayerInPlace);
    }
    void OnDisable()
    {
        playerShouldDie.Unsubscribe(OnPlayerDeathRequested);
        freezePlayerInPlace.Unsubscribe(OnFreezePlayerInPlace);
    }

    void Update()
    {
        pi.Position = transform.position;
        pi.Velocity = prb.linearVelocity;

        playthroughStats.time += Time.deltaTime;

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
        playerAnimator.Play("death");
        gotoScene.Raise(SceneManager.GetActiveScene().name);
        freezePlayerInPlace.Raise();
        playthroughStats.deaths += 1;
    }

    void OnFreezePlayerInPlace()
    {
        prb.linearVelocity = Vector2.zero;
        prb.angularVelocity = 0f;
        prb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
