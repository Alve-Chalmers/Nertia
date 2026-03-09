using UnityEngine;

public class ExitInfoDisplay : MonoBehaviour
{
    void OnEnable()
    {
#if !UNITY_STANDALONE
        gameObject.SetActive(false);
#endif
    }
}
