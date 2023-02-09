using System;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class HeartCounter : MonoBehaviour
    {
        [Range(3, 25)]
        public int startCount = 3;

        [Range(3, 25)]
        public int maxCount = 3;

        [SerializeField] private Transform _heartsPanel;

        [SerializeField] private GameObject _heartPrefab;

        private Queue<GameObject> _allHearts;

        private Queue<GameObject> _activeHearts;

        public static Action OnFallFruit;

        public static Action OnBonusHeart;

        public void Init()
        {
            _allHearts = new Queue<GameObject>();

            _activeHearts = new Queue<GameObject>();

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

                _activeHearts.Enqueue(newHeart);

                newHeart.SetActive(true);
            }
        }

        public void RemoveHeart()
        {
            if (_activeHearts.Count != 0)
            {
                var removedHeart = _activeHearts.Dequeue();

                removedHeart.SetActive(false);

                _allHearts.Enqueue(removedHeart);
            }

            if (_activeHearts.Count == 0)
            {
                GamePlayController.OnStopGame.Invoke();
            }
        }

        private void OnEnable()
        {
            OnFallFruit += RemoveHeart;

            OnBonusHeart += AddHeart;
        }

        private void OnDisable()
        {
            OnFallFruit -= RemoveHeart;

            OnBonusHeart -= AddHeart;
        }
    }
}
