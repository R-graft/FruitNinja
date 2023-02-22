using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace winterStage
{
    public class LosePopUp : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private UIBlur _loseBlur;

        [SerializeField] private Image _bg;
        [SerializeField] private RectTransform _content;

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
            _loseBlur.gameObject.SetActive(true);
            _gameOverPanel.SetActive(true);

            DOTween.To(() => _loseBlur.Intensity, x => _loseBlur.Intensity = x, 1, 0.3f);

            DOTween.Sequence().Append(_bg.DOFade(0.9f, 1)).Append(_content.DOScaleY(1.2f, 0.2f)).Append(_content.DOScaleY(1, 0.2f));

            _bestScore.text = ProgressController.Instance.BestScore.ToString();

            _currentScore.text = ProgressController.Instance._currentScore.ToString();
        }
        public void Restart()
        {
            _loseBlur.Intensity = 0;

            DOTween.Sequence().Append(_content.DOScaleY(0f, 0.2f)).Append(_bg.DOFade(0, 0.5f)).AppendCallback(() => _gameOverPanel.SetActive(false)); 
        }
    }
}
