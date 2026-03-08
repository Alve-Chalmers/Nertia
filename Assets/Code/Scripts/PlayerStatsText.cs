using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsText : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] PlaythroughStats stats;

    void OnEnable()
    {
        int totalSeconds = (int)stats.time;
        int decimals = (int)((stats.time - totalSeconds) * 100);

        int hours = totalSeconds / 3600;
        int mins = (totalSeconds % 3600) / 60;
        int secs = totalSeconds % 60;
        
        string timeStr = hours > 0
            ? $"Time: {hours:D2}:{mins:D2}:{secs:D2}.{decimals}"
            : $"Time: {mins:D2}:{secs:D2}.{decimals:D2}";

        string deathStr = "Deaths: " + stats.deaths.ToString();

        text.text = timeStr + "\n" + deathStr;
    }
}
