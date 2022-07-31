using UnityEngine;
using TMPro;
public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private TextMeshProUGUI _gameScoreText;

    [SerializeField]
    private TextMeshProUGUI _bestScoreText;

    [SerializeField]
    private ScoreCounter _scoreCounter;

    void Awake()
    {
        _gameOverPanel.SetActive(false);

        GameEvents.gameOver.AddListener(ActivePanel);
    }

    private void SetGameOverScore()
    {
        _gameScoreText.text = _scoreCounter.currentScore.ToString();

        _bestScoreText.text = _scoreCounter.bestScore.ToString();

        PlayerPrefs.SetInt("bestScore", _scoreCounter.bestScore);
    }

    private void ActivePanel()
    {
        _gameOverPanel.SetActive(true);

        Invoke("SetGameOverScore", 1.5f);
    }
}
