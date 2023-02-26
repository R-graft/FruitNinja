using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class MagnetBonus : Bonus
    {
        private GameObject _magnetEffect;

        private BlocksController _blocks;

        private BonusController _controller;

        private float _bonusTime;
        public  float magneteTime;

        public MagnetBonus(GameObject magnetEffect, BlocksController blocks, BonusController controller, float bonusTime)
        {
            _magnetEffect = magnetEffect;
            _blocks = blocks;
            _controller = controller;
            _bonusTime = bonusTime;
        }
        public IEnumerator MagneteBonusAction(Transform currentPos)
        {
            magneteTime = _bonusTime;

            Vector2 magnetArea = currentPos.position;

            _magnetEffect.transform.position = magnetArea;

            _magnetEffect.SetActive(true);

            while (magneteTime > 0)
            {
                foreach (var block in _blocks.ActiveBlocks)
                {
                    if (block.StateMashine.CurrentState.GetType() != typeof(MagnetState))
                    {
                        if (block.magneteable && block.gameObject != currentPos.gameObject)
                        {
                            block.StateMashine.SetState(new MagnetState(block, magnetArea));
                        }
                    }
                }

                magneteTime -= Time.deltaTime;

                yield return null;
            }

            foreach (var block in _blocks.AllBlocks)
            {
                if (block.StateMashine.CurrentState is MagnetState)
                {
                    block.StateMashine.SetState(new ActiveState(block, block.currentDirection, block.currentRotation, block.currentScale));
                }
            }

            _controller._isMagnete = false;

            _magnetEffect.SetActive(false);
        }
    }
}
