using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInfo pi;
    [SerializeField] Rigidbody2D prb;
    [SerializeField] SOEvent outsideCameraEvent;

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
    }

    void OnPlayerDeathRequested() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
