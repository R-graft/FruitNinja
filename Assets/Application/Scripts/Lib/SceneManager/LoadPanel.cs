using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace winterStage
{
    public class LoadPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadIcon;

        [SerializeField]
        private Image _loadPanel;

        public void StartLoad()
        {
            _loadPanel.DOFade(1, 0.3f);

            DOTween.Sequence().Append(_loadIcon.transform.DOScale(Vector2.one, 0.3f)).
                Append(_loadIcon.transform.DORotate(new Vector3(0, 0, -720), 1, RotateMode.WorldAxisAdd).SetLoops(2));
        }

        public void EndLoad()
        {
            _loadIcon.transform.DOScale(Vector2.zero, 0.3f);

            _loadPanel.DOFade(0, 0.3f).OnComplete(() => gameObject.SetActive(false));
        }
    }
}