using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] InputActionReference restartAction;
    [SerializeField] SOEventString gotoScene;
    [SerializeField] SOEventBool pause;
    [SerializeField] SOEvent freezePlayer;
    [SerializeField] SceneField sceneToGoTo;

    void OnEnable()
    {
        restartAction.action.performed += Restart;
    }

    private void Restart(InputAction.CallbackContext _)
    {
        pause.Raise(false);
        freezePlayer.Raise();
        gotoScene.Raise(sceneToGoTo);
    }
}
