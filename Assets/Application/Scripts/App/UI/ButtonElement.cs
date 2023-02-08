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

        protected const float animateDuration = 0.3f;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (OnDownAction == null)
                return;

            //AudioController.Instance.GetButtonClickSound();

            Animaton();
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

        public virtual void Animaton() =>
            transform.DOShakeRotation(animateDuration, 90).OnComplete(OnDownAction.Invoke);
    }
}
