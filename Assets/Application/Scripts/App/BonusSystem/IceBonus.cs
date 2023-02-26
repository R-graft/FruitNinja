using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class IceBonus : Bonus
    {
        private GameObject _iceEffect;

        private BonusController _controller;

        private List<Block> _allBlocks;

        private const float IceSpeedModificator = 0.3f;

        private float _bonusTime;
        public float iceTime;

        public IceBonus(GameObject iceEffect, BonusController controller, List<Block> allBlocks, float bonusTime)
        {
            _iceEffect = iceEffect;
            _bonusTime = bonusTime;
            _controller = controller;
            _allBlocks = allBlocks;

        }
        public IEnumerator IceBonusAction()
        {
            iceTime = _bonusTime;

            _iceEffect.SetActive(true);

            foreach (var block in _allBlocks)
            {
                block.mover.SetForce(IceSpeedModificator);
            }

            while (iceTime > 0)
            {
                iceTime -= Time.deltaTime;

                yield return null;
            }

            foreach (var block in _allBlocks)
            {
                block.mover.SetForce(1);
            }

            _iceEffect.SetActive(false);

            _controller._isIce = false;
        }
    }
}
