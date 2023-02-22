using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class HeartBonus : Bonus
    {
        private GameObject _heartEffect;

        private HeartCounter _heartCounter;

        private Transform _controllerTransform;

        private Queue<GameObject> _hearts = new Queue<GameObject>();

        private float _rightScreenEdge;

        private float _upScreenEdge = 3.8f;

        private float _offset;

        private int _heartsPoolCount = 7;

        public HeartBonus(GameObject heartEffect, HeartCounter heartCounter, Transform controller, int heartsPoolCount)
        {
            _heartEffect = heartEffect;
            _heartCounter = heartCounter;
            _heartsPoolCount = heartsPoolCount;
            _controllerTransform = controller;

            HeartsInit();

            _rightScreenEdge = ScreenSizeHandler.rightScreenEdge;

            _offset = _rightScreenEdge * 2 / 15;
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

            Vector2 targetPosition = new Vector2(_rightScreenEdge - (_heartCounter.CurrentHeart +1) * _offset, _upScreenEdge);

            DOTween.Sequence().Append(currentHeart.transform.DOMove(targetPosition, 1)).
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
