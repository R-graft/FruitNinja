using System.Collections;
using UnityEngine;


public class ScoreCounter : MonoBehaviour
{
    private int _startScore;

    private int _scoreRevard;

    public int currentScore { get; private set; }

    public int bestScore { get; private set; }

    void Awake()
    {
        _startScore = 0;

        _scoreRevard = 50;

        bestScore = PlayerPrefs.GetInt("bestScore");

        GameEvents.fruitSlashed.AddListener(SetScore);
    }

    private void Start()
    {
        currentScore = _startScore;
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

            currentScore++;

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
            }
        }
        yield break;
    }
}
