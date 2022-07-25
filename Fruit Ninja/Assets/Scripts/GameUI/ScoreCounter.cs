using System.Collections;
using UnityEngine;


public class ScoreCounter : MonoBehaviour
{
    private int _startScore;

    private int _scoreRevard;

    public int _currentScore { get; private set; }

    public int _bestCurrentScore { get; private set; }

    void Awake()
    {
        _startScore = 0;

        _scoreRevard = 50;

        _bestCurrentScore = PlayerPrefs.GetInt("bestScore");

        GameEvents.fruitSlashed.AddListener(SetScore);
    }

    private void Start()
    {
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
