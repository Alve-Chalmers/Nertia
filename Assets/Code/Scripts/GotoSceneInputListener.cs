using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GotoSceneInputListener : MonoBehaviour
{
    [SerializeField] InputActionReference inputAction;
    [SerializeField] SOEventString gotoScene;
    [SerializeField] SOEventBool pause;
    [SerializeField] SOEvent freezePlayer;
    [SerializeField] SceneField sceneToGoTo;

    void OnEnable()
    {
        inputAction.action.performed += Restart;
    }

    void OnDisable()
    {
        inputAction.action.performed -= Restart;
    }

    private void Restart(InputAction.CallbackContext _)
    {
        pause.Raise(false);
        freezePlayer.Raise();
        gotoScene.Raise(sceneToGoTo);
    }
}
