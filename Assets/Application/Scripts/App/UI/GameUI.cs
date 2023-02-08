using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace winterStage
{
    public class GameUI : MonoBehaviour 
    {
        [SerializeField] private TextMeshProUGUI _bestScoreView;
        [SerializeField] private TextMeshProUGUI _currentScoreView;
        [SerializeField] private Transform _gameOverPanel;

        private int _bestScoreValue;

        private int _currentScoreValue;

        private const float _scoreTimescale = 0.01f;
        public void Init()
        {
            if (!ProgressController.Instance)
            {
                Debug.Log("ProgressController not exist");
                return;
            }

            _bestScoreValue = ProgressController.Instance.BestScore;

            _bestScoreView.text = _bestScoreValue.ToString();

            ProgressController.Instance.SetGameUI(this);
        }
        public void Restart()
        {
            _gameOverPanel.gameObject.SetActive(false);

            _gameOverPanel.localScale = Vector3.right;
            
            _currentScoreValue = 0;

            _currentScoreView.text = _currentScoreValue.ToString();
        }

        public void SetGameOver()
        {
            _gameOverPanel.gameObject.SetActive(true);

            DOTween.Sequence().Append(_gameOverPanel.DOScaleY(1.2f, 0.2f)).Append(_gameOverPanel.DOMoveY(1, 0.2f));
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
