using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private TextMeshProUGUI _bestScore;

    private int _currentScore;

    private int _bestGameScore;

    public void CheckScore()
    {
        if (_currentScore > _bestGameScore)
        {
            _bestGameScore = _currentScore;

            PlayerPrefs.SetInt("best score", _bestGameScore);
        }
    }

    private void LoadBestScore()
    {
        _bestGameScore = PlayerPrefs.GetInt("best score");

        _bestScore.text = _bestGameScore.ToString();
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }
    void Start()
    {
        
    }

    public void SceneChanger(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
