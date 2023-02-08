using DG.Tweening;
using UnityEngine;

namespace winterStage
{
    public class LoadPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadIcon;

        private void OnEnable()
        {
            InAnimation();
        }
        public void InAnimation() => _loadIcon.transform.DORotate(new Vector3(0, 0, -720), 1, RotateMode.WorldAxisAdd).SetLoops(-1).SetLink(gameObject);  

    }
}