using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSaveLoader : MonoBehaviour
{
    [SerializeField] PlaythroughStats playthroughStats;
    [SerializeField] PlaythroughStats highScorePlaythroughStats;
    [SerializeField] SceneField defaultStartingLevel;

    [SerializeField] Text continueText;
    [SerializeField] Color grayedoutColor;

    [SerializeField] InputActionReference newGame;
    [SerializeField] InputActionReference continueGame;
    [SerializeField] SOEventString gotoScene;

    string levelToStartAt;

    bool allowedToContinue;

    void Awake()
    {
        allowedToContinue = PlayerPrefs.HasKey("LevelToStartAt");

        if (!allowedToContinue)
            continueText.color = grayedoutColor;

        levelToStartAt = PlayerPrefs.GetString("LevelToStartAt", defaultStartingLevel);
        playthroughStats.time = PlayerPrefs.GetFloat("PlaythroughStats_time", 0);
        playthroughStats.deaths = PlayerPrefs.GetInt("PlaythroughStats_deaths", 0);

        highScorePlaythroughStats.time = PlayerPrefs.GetFloat("HighScorePlaythroughStats_time", -1);
        highScorePlaythroughStats.deaths = PlayerPrefs.GetInt("HighScorePlaythroughStats_deaths", -1);
    }

    void OnEnable()
    {
        newGame.action.performed += OnNewGame;
        continueGame.action.performed += OnContinueGame;
    }

    void OnDisable()
    {
        newGame.action.performed -= OnNewGame;
        continueGame.action.performed -= OnContinueGame;
    }

    void OnNewGame(InputAction.CallbackContext _) => gotoScene.Raise(defaultStartingLevel);

    void OnContinueGame(InputAction.CallbackContext _)
    {
        if (allowedToContinue)
            gotoScene.Raise(levelToStartAt);
    }
}
