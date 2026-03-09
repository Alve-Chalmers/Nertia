using UnityEngine;
using UnityEngine.UI;

public class HighscorePlayerStatsText : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] PlaythroughStats highScoreStats;

    void OnEnable()
    {
        Debug.Log(highScoreStats.time);
        if (highScoreStats.time < 0) // has not stored highscore before
        {
            text.text = "";
            return;
        }

        int totalSeconds = (int)highScoreStats.time;
        int decimals = (int)((highScoreStats.time - totalSeconds) * 100);

        int hours = totalSeconds / 3600;
        int mins = (totalSeconds % 3600) / 60;
        int secs = totalSeconds % 60;
        
        string timeStr = hours > 0
            ? $"Time: {hours:D2}:{mins:D2}:{secs:D2}.{decimals}"
            : $"Time: {mins:D2}:{secs:D2}.{decimals:D2}";

        string deathStr = "Deaths: " + highScoreStats.deaths.ToString();

        text.text = "Quickest run" + "\n" + timeStr + "\n" + deathStr;
    }
}
