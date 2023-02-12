using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace winterStage
{
    public class ButtonElement : Button
    {
        protected Action OnDownAction;

        protected Action OnUpAction;

        protected const float animateDuration = 0.2f;

        private bool _isActive;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!_isActive || OnDownAction == null)
                return;

            //AudioController.Instance.GetButtonClickSound();

            DownAction();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (OnUpAction == null)
                return;
            //AudioController.Instance.GetButtonClickSound();

            OnUpAction.Invoke();
        }

        public void SetDownAction(Action act, bool add) => OnDownAction = add ? OnDownAction += act : OnDownAction -= act;

        public void SetUpAction(Action act, bool add) => OnUpAction = add ? OnUpAction += act : OnUpAction -= act;

        private void DownAction()
        {
            _isActive = false;

            Animaton();
        }
        public virtual void Animaton()
        {
            DOTween.Sequence().Append(transform.DOShakeRotation(animateDuration, 50)).AppendCallback(OnDownAction.Invoke);
        }

        protected override void OnEnable()
        {
            _isActive = true;
        }
    }
}
