using UnityEngine;

public class RatNPC : MonoBehaviour
{
    [SerializeField] GameObject boxingGloveUnlock;

    [SerializeField] SOEvent ratSpawnBoxingGlove;

    void OnEnable()
    {
        ratSpawnBoxingGlove.Subscribe(OnBox);
    }

    void OnDisable()
    {
        ratSpawnBoxingGlove.Unsubscribe(OnBox);
    }

    void OnBox()
    {
        if (boxingGloveUnlock != null)
            boxingGloveUnlock.SetActive(true);
    }
}
