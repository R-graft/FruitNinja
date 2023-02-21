using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class HeartCounter : MonoBehaviour
    {
        [Range(1, 8)]
        public int startCount = 3;

        [Range(1, 8)]
        public int maxCount = 3;

        [SerializeField] private Transform _heartsPanel;

        [SerializeField] private GameObject _heartPrefab;

        private List<GameObject> _hearts;

        public int CurrentHeart { get; private set; }

        public static Action OnLoseHeart;

        public static Action OnBonusHeart;

        public void Init()
        {
            _hearts = new List<GameObject>();

            for (int i = 0; i < maxCount; i++)
            {
                var newHeart = Instantiate(_heartPrefab, _heartsPanel);

                newHeart.SetActive(false);

                _hearts.Add(newHeart);
            }

            for (int i = 0; i < startCount; i++)
            {
                AddHeart();
            }
        }

        public void AddHeart()
        {
            if (CurrentHeart < maxCount)
            {
                var newHeart = _hearts[CurrentHeart];

                newHeart.SetActive(true);

                newHeart.transform.DOScale(new Vector2(1.1f, 1), 0.6f);

                CurrentHeart++;
            }
        }

        public void RemoveHeart()
        {
            if (CurrentHeart > 0)
            {
                var removedHeart = _hearts[CurrentHeart - 1];

                removedHeart.transform.DOScale(Vector3.zero, 0.4f).OnComplete(()=> removedHeart.SetActive(false));

                CurrentHeart--;
            }

            if (CurrentHeart == 0)
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
