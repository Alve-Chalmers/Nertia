using UnityEngine;
using UnityEngine.UI;

public class HighscoreSetter : MonoBehaviour
{
    [SerializeField] PlaythroughStats stats;
    [SerializeField] PlaythroughStats highScoreStats;

    [SerializeField] GameObject newHighscoreText;

    void Awake()
    {
        if (highScoreStats.time < 0)
        {
            highScoreStats.time = stats.time;
            highScoreStats.deaths = stats.deaths;
        }
        else if (stats.time < highScoreStats.time)
        {
            highScoreStats.time = stats.time;
            highScoreStats.deaths = stats.deaths;
            
            newHighscoreText.SetActive(true);
        }

        PlayerPrefs.SetFloat("HighScorePlaythroughStats_time", highScoreStats.time);
        PlayerPrefs.SetInt("HighScorePlaythroughStats_deaths", highScoreStats.deaths);
    }
}
