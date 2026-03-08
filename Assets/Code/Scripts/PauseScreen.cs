using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] SOEventBool setPause;
    [SerializeField] GameObject thingsToHide;


    void OnEnable()
    {
        setPause.Subscribe(OnPause);
    }

    void OnDisable()
    {
        setPause.Unsubscribe(OnPause);
    }

    void OnPause(bool pause)
    {
        thingsToHide.SetActive(pause);
    }
}
