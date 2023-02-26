using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BombBonus : Bonus
    {
        private Transform _bombEffect;

        private List<Block> _activeBlocks;

        private float _maxBombDistance = 30;
        private float _bombForce = 5;

        public BombBonus(Transform bobmEffect, List<Block> activeBlocks, float maxBombDistance, float bombForce)
        {
            _bombEffect= bobmEffect;

            _activeBlocks = activeBlocks;

            _maxBombDistance = maxBombDistance;
            _bombForce = bombForce;
        }
        public void BombBonusAction(Transform bombTransform)
        {
            Vector3 bombPosition = bombTransform.position;

            DOTween.Sequence().
                Append(_bombEffect.DOScale(new Vector2(1.2f, 1.2f), 0.1f)).
                Append(_bombEffect.DOScale(Vector2.one, 0.2f)).
                Append(_bombEffect.DOScale(new Vector2(1.1f, 1.1f), 0.2f)).
                Append(_bombEffect.DOScale(Vector2.one, 0.3f));

            foreach (var block in _activeBlocks)
            {
                if (bombTransform.gameObject != block.gameObject)
                {
                    Vector3 directionToBomb = block.transform.position - bombPosition;

                    float distanceToBlock = directionToBomb.sqrMagnitude;

                    if (distanceToBlock < _maxBombDistance)
                    {
                        block.mover.SetDirection(directionToBomb.normalized * _bombForce);
                    }
                }
            }
        }
    }
}
