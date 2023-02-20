using UnityEngine;
using DG.Tweening;

namespace winterStage
{
    public abstract class SplashView : MonoBehaviour
    {
        [SerializeField] private GameObject _splash;

        [SerializeField] private ParticleSystem _particleEmiter;

        public TrailRenderer flyingTrail;

        public static bool foolMode = true;

        public virtual void ActivateSplash()
        {
            _particleEmiter.Play();

            _splash.SetActive(true);

            var _splashSequence = DOTween.Sequence();
            _splashSequence.AppendInterval(0.15f).OnComplete(() => _splash.SetActive(false));
        }
    }
}
