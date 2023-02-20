using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class HeartBonus : Bonus
    {
        private GameObject _heartEffect;

        private Transform _heartCounter;

        private Transform _controllerTransform;

        private Queue<GameObject> _hearts = new Queue<GameObject>();

        private int _heartsPoolCount = 7;

        public HeartBonus(GameObject heartEffect, Transform heartCounter, Transform controller, int heartsPoolCount)
        {
            _heartEffect = heartEffect;
            _heartCounter = heartCounter;
            _heartsPoolCount = heartsPoolCount;
            _controllerTransform = controller;

            HeartsInit();
        }
        private void HeartsInit()
        {
            if (_hearts != null && _hearts.Count != 0)
            {
                foreach (var item in _hearts)
                {
                    Object.Destroy(item);
                }
            }

            _hearts = new Queue<GameObject>();

            for (int i = 0; i < _heartsPoolCount; i++)
            {
                var newHeart = Object.Instantiate(_heartEffect, _controllerTransform);

                newHeart.SetActive(false);

                _hearts.Enqueue(newHeart);
            }
        }

        public void HeartBonusAction(Vector3 heartPos)
        {
            var currentHeart = GetHeart();

            currentHeart.transform.position = heartPos;

            DOTween.Sequence().Append(currentHeart.transform.DOMove(_heartCounter.position, 1)).
                AppendCallback(() => EndHeart(currentHeart));
        }

        private GameObject GetHeart()
        {
            var heart = _hearts.Dequeue();

            heart.SetActive(true);

            return heart;
        }

        private void EndHeart(GameObject heart)
        {
            heart.SetActive(false);

            _hearts.Enqueue(heart);

            HeartCounter.OnBonusHeart.Invoke();
        }
    }
}
