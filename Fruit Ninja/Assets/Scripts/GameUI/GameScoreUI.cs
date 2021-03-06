using UnityEngine;
using TMPro;

public class GameScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _currentScoreText;

    [SerializeField]
    private TextMeshProUGUI _bestScoreText;

    [SerializeField]
    private ScoreCounter _scoreCounter;

    private void CheckScore()
    {
        _currentScoreText.text = _scoreCounter.currentScore.ToString();

        _bestScoreText.text = _scoreCounter.bestScore.ToString();
    }

    void FixedUpdate()
    {
        CheckScore();
    }
}
