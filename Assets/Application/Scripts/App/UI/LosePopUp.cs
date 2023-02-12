using DG.Tweening;
using TMPro;
using UnityEngine;

namespace winterStage
{
    public class LosePopUp : MonoBehaviour
    {
        [SerializeField] private RectTransform _gameOverPopUp;

        [SerializeField] private ButtonElement _restart;
        [SerializeField] private ButtonElement _menu;

        [SerializeField] private TextMeshProUGUI _bestScore;
        [SerializeField] private TextMeshProUGUI _currentScore;

        public void Init()
        {
            Restart();

            _menu.SetDownAction(()=> ScenesManager.Instance.LoadScene(SCENELIST.Menu), true);

            _restart.SetDownAction(()=> GamePlayController.OnRestart?.Invoke(), true);
        }

        public void GameOver()
        {
            _gameOverPopUp.gameObject.SetActive(true);

            DOTween.Sequence().Append(_gameOverPopUp.DOScaleY(1.2f, 0.2f)).Append(_gameOverPopUp.DOScaleY(1, 0.2f));

            _bestScore.text = ProgressController.Instance.BestScore.ToString();

            _currentScore.text = ProgressController.Instance._currentScore.ToString();
        }
        public void Restart()
        {
            _gameOverPopUp.gameObject.SetActive(false);

            _gameOverPopUp.localScale = Vector3.right;
        }
    }
}
