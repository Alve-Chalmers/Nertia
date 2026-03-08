using UnityEngine;

public class PlaythroughStatsResetter : MonoBehaviour
{
    [SerializeField] PlaythroughStats playthroughStats;

    void Awake()
    {
        playthroughStats.time = 0;
        playthroughStats.deaths = 0;
    }
}
