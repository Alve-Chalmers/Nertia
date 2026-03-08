using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSkipper : MonoBehaviour
{
    [SerializeField] private InputActionReference nextSceneAction;
    [SerializeField] private InputActionReference prevSceneAction;

    [SerializeField] SOEventString gotoScene;

    [SerializeField] SceneField firstScene;
    [SerializeField] SceneField lastScene;

    private int minBuildIndex;
    private int maxBuildIndex;

    private void Awake()
    {
        minBuildIndex = GetBuildIndex(firstScene);
        maxBuildIndex = GetBuildIndex(lastScene);
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
        int targetIndex = Mathf.Clamp(current + offset, minBuildIndex, maxBuildIndex);

        if (targetIndex == current)
            return;

        string scenePath = SceneUtility.GetScenePathByBuildIndex(targetIndex);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        gotoScene.Raise(sceneName);
    }

    private static int GetBuildIndex(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            if (System.IO.Path.GetFileNameWithoutExtension(path) == sceneName)
                return i;
        }
        return -1;
    }
}