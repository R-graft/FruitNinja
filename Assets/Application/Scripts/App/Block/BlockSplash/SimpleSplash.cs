using DG.Tweening;
using UnityEngine;

namespace winterStage
{
    public class SimpleSplash : SplashView
    {
        [SerializeField] private Transform _blot;

        [SerializeField] private SpriteRenderer _blotSprite;

        [SerializeField] private GameObject _score;

        public override void ActivateSplash()
        {
            base.ActivateSplash();

            if (foolMode)
            {
                _score.SetActive(true);

                var _scoreSequence = DOTween.Sequence().Append(_score.transform.DOScale(new Vector2(0.002f, 0.002f), 0.1f)).
                   AppendInterval(0.3f).Append(_score.transform.DOScale(Vector2.zero, 0.1f));

                _blot.gameObject.SetActive(true);

                var _blotSequence = DOTween.Sequence();
                _blotSequence.Append(_blot.DOLocalMoveY(-2, 2)).Insert(0, _blotSprite.DOFade(0, 2f)).
                    Insert(0, _blot.DOScaleY(2, 2)).OnComplete(() => DeactiveSplash());
            }
        }

        private void DeactiveSplash()
        {
            _score.SetActive(false);
            _blot.gameObject.SetActive(false);

            var blotColor = _blotSprite.color;
            blotColor.a = 1;
            _blotSprite.color = blotColor;

            _blot.transform.localPosition = Vector2.zero;
            _blot.transform.localScale = Vector2.one;
        }
    }
}