using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] InputActionReference restartAction;
    [SerializeField] SOEventString gotoScene;
    [SerializeField] SOEventBool pause;
    [SerializeField] SceneField sceneToGoTo;

    void OnEnable()
    {
        restartAction.action.performed += Restart;
    }

    private void Restart(InputAction.CallbackContext _)
    {
        pause.Raise(false);
        gotoScene.Raise(sceneToGoTo);
    }
}
