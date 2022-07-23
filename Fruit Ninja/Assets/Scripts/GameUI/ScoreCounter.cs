using System.Collections;
using UnityEngine;


public class ScoreCounter : MonoBehaviour
{
    private int _startScore;

    private int _scoreRevard;

    public int _currentScore { get; private set; }

    public int _bestCurrentScore { get; private set; }

    void Start()
    {
        _startScore = 0;

        _scoreRevard = 50;

        StartGame();

        GameEvents.fruitSlashed.AddListener(SetScore);
    }
    private void StartGame()
    {
        _bestCurrentScore = PlayerPrefs.GetInt("best score");

        _currentScore = _startScore;
    }

    private void SetScore()
    {
        StartCoroutine(AddScore());
    }

    private IEnumerator AddScore()
    {
        for (int i = 0; i < _scoreRevard; i++)
        {
            yield return new WaitForSeconds(0.01f);

            _currentScore++;

            if (_currentScore > _bestCurrentScore)
            {
                _bestCurrentScore = _currentScore;
            }
        }
        yield break;
    }
}
