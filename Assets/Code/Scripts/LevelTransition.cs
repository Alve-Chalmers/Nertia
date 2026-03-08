using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] SOEventString gotoScene;
    [SerializeField] Animator animator;

    AsyncOperation asyncLoad;

    void OnEnable()
    {
        gotoScene.Subscribe(OnGotoScene);
    }

    void OnDisable()
    {
        gotoScene.Unsubscribe(OnGotoScene);
    }

    void OnGotoScene(string scene)
    {
        asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;
        animator.Play("LevelTransitionExit");
    }

    void LoadTheScene() {
        asyncLoad.allowSceneActivation = true;
    }
}
