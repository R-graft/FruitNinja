using System.Collections;
using TMPro;
using UnityEngine;

namespace winterStage
{
    public class GameUI : MonoBehaviour 
    {
        [SerializeField] private TextMeshProUGUI _bestScoreView;
        [SerializeField] private TextMeshProUGUI _currentScoreView;

        [SerializeField] private GameObject _seriesCounter;

        [SerializeField] private LosePopUp _lose;

        private int _bestScoreValue;

        private int _currentScoreValue;

        private const float _scoreTimescale = 0.01f;
        public void Init()
        {
            if (ProgressController.Instance != null)
            {
                _bestScoreValue = ProgressController.Instance.BestScore;

                ProgressController.Instance.SetGameUI(this);
            }
            else
            {
                Debug.Log("ProgressController not exist");
            }

            _lose.Init();

            _bestScoreView.text = _bestScoreValue.ToString();

            Instantiate(_seriesCounter, transform);
        }

        public void SetGameOver()
        {
            _lose.GameOver();
        }

        public void Restart()
        {
            _currentScoreValue = 0;

            _currentScoreView.text = _currentScoreValue.ToString();

            _lose.Restart();
        }

        public void SetNewScore(int newScore)
        {
            StartCoroutine(IncrementScore(newScore));
        }

        public IEnumerator IncrementScore(int scoreValue)
        {
            while (scoreValue != 0)
            {
                yield return new WaitForSeconds(_scoreTimescale);

                _currentScoreValue++;

                _currentScoreView.text = _currentScoreValue.ToString();

                if (_currentScoreValue > _bestScoreValue)
                {
                    _bestScoreValue = _currentScoreValue;

                    _bestScoreView.text = _bestScoreValue.ToString();
                }
                scoreValue--;
            }
        }
    }
}
