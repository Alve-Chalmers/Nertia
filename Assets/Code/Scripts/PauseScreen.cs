using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] SOEventBool setPause;
    [SerializeField] GameObject thingsToHide;


    void Awake()
    {
        setPause.Subscribe(OnPause);
    }

    void OnDestroy()
    {
        setPause.Unsubscribe(OnPause);
    }

    void OnPause(bool pause)
    {
        thingsToHide.SetActive(pause);
    }
}
