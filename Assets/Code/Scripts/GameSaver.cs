using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour
{
    [SerializeField] PlaythroughStats playthroughStats;

    void Awake()
    {
        PlayerPrefs.SetString("LevelToStartAt", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PlaythroughStats_time", playthroughStats.time);
        PlayerPrefs.SetInt("PlaythroughStats_deaths", playthroughStats.deaths);
        PlayerPrefs.Save();
    }
}
