using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _currentScoreText;

    [SerializeField]
    private TextMeshProUGUI _bestScoreText;

    [SerializeField]
    private GameObject _heartPanel;

    [SerializeField]
    private GameObject _heartPrefab;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private SpawnManager _spawnManager;

    private int _scoreRevard = 50;

    void Start()
    {
        GameEvents.fruitLost.AddListener(DecrementHeart);
        GameEvents.fruitSlashed.AddListener(SetScore);

        _bestScoreText.text = PlayerPrefs.GetInt("best score").ToString();
        _currentScoreText.text = "0";

        StartCoroutine(CheckBestScore());
    }

    private void SetScore()
    {
        StartCoroutine("AddScore");
    }
    private IEnumerator AddScore()
    {

        for (int i = 0; i < _scoreRevard; i++)
        {
            yield return new WaitForSeconds(0.01f);

            _currentScoreText.text = (int.Parse(_currentScoreText.text) + 1).ToString();
        }
        yield break;
    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }
    private IEnumerator CheckBestScore()
    {
        int score = int.Parse(_currentScoreText.text);

        int bestScore = int.Parse(_bestScoreText.text);

        while (bestScore > score)
        {
            yield return new WaitForFixedUpdate();

            score = int.Parse(_currentScoreText.text);
        }
        while (_heartPanel.transform.childCount != 0)
        {
            yield return new WaitForFixedUpdate();
            _bestScoreText.text = "Best:" + _currentScoreText.text;
        }
        PlayerPrefs.SetInt("best score", score);
    }
    
    void Update()
    {
    }
    private void DecrementHeart()
    {
        if (_heartPanel.transform.childCount == 0)
        {
            GameOver();
            StopCoroutine(_spawnManager.StartFruitPack());
        }
        else
        {
            Destroy(_heartPanel.transform.GetChild(0).gameObject);
        }
    }
    private void AddHeart()
    {
        Instantiate(_heartPrefab, _heartPanel.transform);
    }
}
