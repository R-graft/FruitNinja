using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BonusController : MonoBehaviour
    {
        [SerializeField] private GameObject _iceEffect;
        [SerializeField] private GameObject _magnetEffect;
        [SerializeField] private GameObject _heartEffect;
        [SerializeField] private Transform _bombEffect;

        [SerializeField] private BlocksController _blocks;

        [SerializeField] private SpawnSystem _spawner;

        [SerializeField] private TransformHandler _transformer;

        [SerializeField] private Transform _heartCounter;

        private Queue<GameObject> _heartsQueue;

        private const float BonusTime = 5;

        public static Action<Transform> OnBombSlash;
        public static Action<Transform> OnMagnetSlash;
        public static Action<Vector3> OnHeartSlash;
        public static Action OnIceSlash;
        public static Action<Vector3> OnBasketSlash;

#region(iceSettings)
        private const float IceSpeedModificator = 0.3f;
        private float _iceTime;
        private bool _isIce;
#endregion

#region(bombSettings)
        private const float MaxBombDistance = 30;
        private const float BombForce = 10;
#endregion

#region(magneteSettings)
        private bool _isMagnete;
        private float _magneteTime;
        #endregion

#region(heartSettings)
        private const int HeartsPoolCount = 7;
#endregion

#region(basketSettings)
private readonly Vector2 FirstDirection = new Vector2(0, 5f);
private readonly Vector2 SecondDirection = new Vector2(5, 5f);
#endregion

        public void Init()
        {
            HeartsInit();
        }
#region(Ice)
        private void IceBonus()
        {
            if (!_isIce)
            {
                StartCoroutine(IceBonusAction());
            }
            else
            {
                _iceTime = BonusTime;
            }
        }

        private IEnumerator IceBonusAction()
        {
            _iceTime = BonusTime;

            _isIce = true;

            _iceEffect.SetActive(true);

            MoveBlock.SetForce(IceSpeedModificator);

            while (_iceTime > 0)
            {
                _iceTime -= Time.deltaTime;

                yield return null;
            }

            MoveBlock.SetForce(1);

            _iceEffect.SetActive(false);

            _isIce = false;
        }
#endregion

#region(Magnet)
        private void MagnetBonus(Transform magnetPos)
        {
            if (!_isMagnete)
            {
                StartCoroutine(MagneteBonusAction(magnetPos));
            }
            else
            {
                _magneteTime = BonusTime;
            }
        }
        private IEnumerator MagneteBonusAction(Transform currentPos)
        {
            _isMagnete = true;

            _magneteTime = BonusTime;

            Vector3 magnetArea = currentPos.position;

            _magnetEffect.transform.position = magnetArea;

            _magnetEffect.SetActive(true);

            while (_magneteTime > 0)
            {
                foreach (var block in _blocks.ActiveBlocks)
                {
                    if (block.StateMashine.CurrentState.GetType() != typeof(MagnetState))
                    {
                        if (!block.TryGetComponent(out BombBlock _) && block.gameObject != currentPos.gameObject)
                        {
                            block.StateMashine.SetState(new MagnetState(block, magnetArea));
                        }
                    }
                }

                _magneteTime -= Time.deltaTime;

                yield return null;
            }

            foreach (var block in _blocks.ActiveBlocks)
            {
                block.StateMashine.SetState(new ActiveState(block, block.currentDirection, block.currentRotation, block.currentScale));
            }

            _isMagnete = false;

            _magnetEffect.SetActive(false);
        }
        #endregion

#region(Heart)
        private void HeartBonus(Vector3 heartPos)
        {
            var currentHeart = GetHeart();

            currentHeart.transform.position = heartPos;

            DOTween.Sequence().Append(currentHeart.transform.DOMove(_heartCounter.position, 1)).
                AppendCallback(() => EndHeart(currentHeart));
        }

        private void HeartsInit()
        {
            if (_heartsQueue != null && _heartsQueue.Count != 0)
            {
                foreach (var item in _heartsQueue)
                {
                    Destroy(item);
                }
            }

            _heartsQueue = new Queue<GameObject>();

            for (int i = 0; i < HeartsPoolCount; i++)
            {
                var newHeart = Instantiate(_heartEffect, transform);

                newHeart.SetActive(false);

                _heartsQueue.Enqueue(newHeart);
            }
        }

        private GameObject GetHeart()
        {
            var heart = _heartsQueue.Dequeue();

            heart.SetActive(true);

            return heart;
        }

        private void EndHeart(GameObject heart)
        {
            heart.SetActive(false);

            _heartsQueue.Enqueue(heart);

            HeartCounter.OnBonusHeart.Invoke();
        }

#endregion

#region(Bomb)
        private void BombBonus(Transform bombTransform)
        {
            Vector3 bombPosition = bombTransform.position;

            DOTween.Sequence().
                Append(_bombEffect.DOScale(new Vector2(1.2f, 1.2f), 0.1f)).
                Append(_bombEffect.DOScale(Vector2.one, 0.2f)).
                Append(_bombEffect.DOScale(new Vector2(1.1f, 1.1f), 0.2f)).
                Append(_bombEffect.DOScale(Vector2.one, 0.3f));

            foreach (var block in _blocks.ActiveBlocks)
            {
                if (bombTransform.gameObject != block.gameObject)
                {
                    Vector3 directionToBomb = block.transform.position - bombPosition;

                    float distanceToBlock = directionToBomb.sqrMagnitude;

                    if (distanceToBlock < MaxBombDistance)
                    {
                        block.mover.SetDirection(directionToBomb.normalized * BombForce);
                    }
                }
            }
        }
        #endregion

#region(Basket)
        private void BasketBonus(Vector3 basketPos)
        {
            var newBasket = _spawner.GetCurrentPack();

            while (newBasket.Count > 0 ) 
            {
                var block = newBasket.Dequeue();

                if (block.TryGetComponent(out IBonusBlock _))
                {
                    _blocks.DeactivateBlock(block);

                    continue;
                }

                block.transform.position = new Vector3(basketPos.x, basketPos.y+2) ;

                var newRotateValue = _transformer.GetRandomRotateValue();

                var newScaleValue = _transformer.GetRandomScaleValue();

                float directionX = UnityEngine.Random.Range(FirstDirection.x, SecondDirection.x);
                float directionY = UnityEngine.Random.Range(FirstDirection.y, SecondDirection.y);

                Vector2 newDirection = new Vector2(directionX, directionY);

                block.StateMashine.SetState(new ActiveState(block, newDirection, newRotateValue, newScaleValue));

                _blocks.AddBlock(block, true);
            }
        }

#endregion

        private void OnEnable()
        {
            OnBombSlash += BombBonus;
            OnIceSlash += IceBonus;
            OnMagnetSlash += MagnetBonus;
            OnHeartSlash += HeartBonus;
            OnBasketSlash += BasketBonus;
        }

        private void OnDisable()
        {
            OnBombSlash -= BombBonus;
            OnIceSlash -= IceBonus;
            OnMagnetSlash -= MagnetBonus;
            OnHeartSlash -= HeartBonus;
            OnBasketSlash -= BasketBonus;
        }
        
    }
}
