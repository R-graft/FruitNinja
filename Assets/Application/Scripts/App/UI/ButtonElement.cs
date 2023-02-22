using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace winterStage
{
    public class ButtonElement : Button
    {
        protected Action OnDownAction;

        protected Action OnUpAction;

        protected const float animateDuration = 0.2f;

        private bool _isActive = true;

        private static bool PushBlock;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!_isActive || OnDownAction == null || PushBlock)
                return;

            DownAction();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (OnUpAction == null)
                return;

            OnUpAction.Invoke();
        }

        public void SetDownAction(Action act, bool add) => OnDownAction = add ? OnDownAction += act : OnDownAction -= act;

        public void SetUpAction(Action act, bool add) => OnUpAction = add ? OnUpAction += act : OnUpAction -= act;

        private void DownAction()
        {
            Animaton();
        }
        public virtual void Animaton()
        {
            DOTween.Sequence().Append(transform.DOScale(new Vector2(0.8f, 0.8f),0.1f)).Append(transform.DOScale(Vector2.one, 0.1f))
                .InsertCallback(1, OnDownAction.Invoke).
                InsertCallback(0, ()=> _isActive = false).
                InsertCallback(0, ()=> PushBlock = true)
                .AppendInterval(0.5f).
                OnComplete(()=> _isActive = true).
                AppendCallback(()=> PushBlock = false);
        }
    }
}
