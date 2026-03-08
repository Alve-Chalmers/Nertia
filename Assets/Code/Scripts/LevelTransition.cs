using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] SOEventString gotoScene;
    [SerializeField] Animator animator;

    AsyncOperation asyncLoad;

    bool isTransitioning = false;

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
        if (isTransitioning)
            return;
        isTransitioning = true;
        if (scene == SceneManager.GetActiveScene().name) {
            animator.Play("LevelTransitionReset");
        }
        else {
            animator.Play("LevelTransitionExit");
        }
        asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;
    }

    void LoadTheScene() {
        isTransitioning = false;
        asyncLoad.allowSceneActivation = true;
    }
}
