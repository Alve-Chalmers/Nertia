using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private InputActionReference nextSceneAction;
    [SerializeField] private InputActionReference prevSceneAction;

    private int maxBuildIndex;

    private void Awake()
    {
        maxBuildIndex = SceneManager.sceneCountInBuildSettings - 3;
    }

    private void OnEnable()
    {
        nextSceneAction.action.performed += OnNext;
        prevSceneAction.action.performed += OnPrevious;

        nextSceneAction.action.Enable();
        prevSceneAction.action.Enable();
    }

    private void OnDisable()
    {
        nextSceneAction.action.performed -= OnNext;
        prevSceneAction.action.performed -= OnPrevious;

        nextSceneAction.action.Disable();
        prevSceneAction.action.Disable();
    }

    private void OnNext(InputAction.CallbackContext ctx)     => ChangeScene(+1);
    private void OnPrevious(InputAction.CallbackContext ctx) => ChangeScene(-1);

    private void ChangeScene(int offset)
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        int target  = Mathf.Clamp(current + offset, 0, maxBuildIndex);

        if (target != current)
            SceneManager.LoadScene(target);
    }
}