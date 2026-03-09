using UnityEngine;
using UnityEngine.UI;

public class HighscoreSetter : MonoBehaviour
{
    [SerializeField] PlaythroughStats stats;
    [SerializeField] PlaythroughStats highScoreStats;

    [SerializeField] GameObject newHighscoreText;

    void Awake()
    {
        highScoreStats.time = PlayerPrefs.GetFloat("HighScorePlaythroughStats_time", -1);
        highScoreStats.deaths = PlayerPrefs.GetInt("HighScorePlaythroughStats_deaths", -1);

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
        PlayerPrefs.Save();
    }
}
