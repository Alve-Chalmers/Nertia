using UnityEngine;

public class NPCSpawnItem : MonoBehaviour
{
    [SerializeField] GameObject itemToSetActive;

    [SerializeField] SOEvent spawnItemEvent;

    void OnEnable()
    {
        spawnItemEvent.Subscribe(OnBox);
    }

    void OnDisable()
    {
        spawnItemEvent.Unsubscribe(OnBox);
    }

    void OnBox()
    {
        if (itemToSetActive != null)
            itemToSetActive.SetActive(true);
    }
}
