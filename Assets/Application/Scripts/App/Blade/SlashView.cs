using UnityEngine;
using DG.Tweening;

namespace winterStage
{
    public class SlashView : MonoBehaviour
    {
        [SerializeField] private Transform _blot;
        [SerializeField] private SpriteRenderer _blotSprite;

        [SerializeField] private ParticleSystem _particleEmiter;

        [SerializeField] private Transform _splash;

        [SerializeField] private RectTransform _score;


        public void ActivateSplash()
        {
            //_particleEmiter.Play();

            var _splashSequence = DOTween.Sequence();
            _splashSequence.Append(_splash.DOScale(Vector2.one, 0.1f)).Append(_splash.DOScale(Vector2.zero, 0.1f));

            if (_blot != null)
            {
                var _blotSequence = DOTween.Sequence();
                _blotSequence.Append(_blot.DOScale(Vector2.one, 0.1f)).Insert(1, _blot.DOLocalMoveY(-1, 1)).
                    Insert(1, _blotSprite.DOFade(0, 2)).Insert(1, _blot.DOScaleY(2, 1)).OnComplete(() => RestoreBlot());
            }

            //var _scoreSequence = DOTween.Sequence();
            //_scoreSequence.Append(_score.DOScale(new Vector2(45, 45), 0.5f)).Append(_score.DOScale(Vector2.zero, 0.5f));
        }

        private void RestoreBlot()
        {
            _blotSprite.DOFade(1, 0);
            _blot.transform.localPosition = Vector2.zero;
            _blot.transform.localScale = Vector2.zero;
        }
    }
}
