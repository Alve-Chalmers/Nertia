using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveLoader : MonoBehaviour
{
    [SerializeField] PlaythroughStats playthroughStats;
    [SerializeField] SceneField defaultStartingLevel;

    void Awake()
    {
        string levelToStartAt = PlayerPrefs.GetString("LevelToStartAt", defaultStartingLevel);
        playthroughStats.time = PlayerPrefs.GetFloat("PlaythroughStats_time", 0);
        playthroughStats.deaths = PlayerPrefs.GetInt("PlaythroughStats_deaths", 0);
        
        SceneManager.LoadScene(levelToStartAt);
    }
}
