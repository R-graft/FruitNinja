using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class HeartCounter : MonoBehaviour
    {
        [Range(1, 25)]
        public int startCount = 3;

        [Range(1, 25)]
        public int maxCount = 3;

        [SerializeField] private Transform _heartsPanel;

        [SerializeField] private GameObject _heartPrefab;

        private Queue<GameObject> _allHearts;

        private Stack<GameObject> _activeHearts;

        public static Action OnLoseHeart;

        public static Action OnBonusHeart;

        public void Init()
        {
            _allHearts = new Queue<GameObject>();

            _activeHearts = new Stack<GameObject>();

            for (int i = 0; i < maxCount; i++)
            {
                var newHeart = Instantiate(_heartPrefab, _heartsPanel);

                newHeart.SetActive(false);

                _allHearts.Enqueue(newHeart);
            }

            for (int i = 0; i < startCount; i++)
            {
                AddHeart();
            }
        }

        public void AddHeart()
        {
            if (_allHearts.Count > 0)
            {
                var newHeart = _allHearts.Dequeue();

                _activeHearts.Push(newHeart);

                newHeart.SetActive(true);

                newHeart.transform.DOScale(new Vector2(1.1f, 1), 0.6f);
            }
        }

        public void RemoveHeart()
        {
            if (_activeHearts.Count != 0)
            {
                var removedHeart = _activeHearts.Pop();

                removedHeart.transform.DOScale(Vector3.zero, 0.4f).OnComplete(()=> removedHeart.SetActive(false));

                _allHearts.Enqueue(removedHeart);
            }

            if (_activeHearts.Count == 0)
            {
                GamePlayController.OnStopGame.Invoke();
            }
        }

        private void OnEnable()
        {
            OnLoseHeart += RemoveHeart;

            OnBonusHeart += AddHeart;
        }

        private void OnDisable()
        {
            OnLoseHeart -= RemoveHeart;

            OnBonusHeart -= AddHeart;
        }
    }
}
