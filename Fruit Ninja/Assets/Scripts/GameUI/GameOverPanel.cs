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
    void Start()
    {
        _gameOverPanel.SetActive(false);

        GameEvents.gameOver.AddListener(ActivePanel);
    }

    private void SetScore()
    {
        _gameScoreText.text = _scoreCounter._currentScore.ToString();

        _bestScoreText.text = _scoreCounter._bestCurrentScore.ToString();
    }

    private void ActivePanel()
    {
        _gameOverPanel.SetActive(true);
    }
}
