using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class MimicBonus : Bonus
    {
        private Queue<Block> _currentPack;

        private SpawnSystem _spawner;

        private BlocksController _controller;

        private MimicBlock _mimicType;

        private Block _currentType;

        private Block _nextType;

        private float _mimicTime = 1;

        private int _mimicPackSize = 5;

        private bool _isActive;

        public MimicBonus(SpawnSystem spawner, BlocksController controller)
        {
            _spawner = spawner;

            _controller = controller;
        }

        public void GetCurrentPack()
        {
            var newBasket = new Queue<Block>();

            _currentPack = _spawner.GetCurrentPack(newBasket, false, _mimicPackSize);
        }

        public IEnumerator MimicMove(Block block)
        {
            while (true)
            {
                if (_currentType.StateMashine.CurrentState is CrushState)
                {
                    _mimicType.particleEffect.Stop();

                    break;
                }

                if (_mimicType.StateMashine.CurrentState is DisableState)
                {
                    break;
                }

                else
                {
                    _currentType.transform.position = _mimicType.transform.position;

                    yield return null;
                }
            }
                
            _isActive = false;

            _mimicType.isBonus = true;

            while (_currentPack.Count > 0)
            {
                _controller.DeactivateBlock(_currentPack.Dequeue());

                _controller.ActiveBlocks.Remove(block);
            }
        }
        public IEnumerator MimicAction(MimicBlock block)
        {
            _isActive = true;

            _mimicType = block;

            GetCurrentPack();

            ChangeType();

            while (_isActive)
            {
                if (_isActive)
                {
                    ChangeType();
                }

                yield return new WaitForSeconds(_mimicTime);
            }
        }

        private void ChangeType()
        {
            if (_currentPack.Count > 0)
            {
                _nextType = _currentPack.Dequeue();

                if (_nextType.blockTag == "mimic")
                {
                    _controller.DeactivateBlock(_nextType);

                    ChangeType();

                    return;
                }

                if (_currentType != null)
                {
                    if (_currentType.TryGetComponent(out BoostSplash trail))
                    {
                        trail.flyingTrail.enabled = true;
                    }

                    _controller.DeactivateBlock(_currentType);

                    _controller.ActiveBlocks.Remove(_currentType);
                }

                if (_nextType.TryGetComponent(out BoostSplash boost))
                {
                    boost.flyingTrail.enabled = false;
                }

                _mimicType.magneteable = _nextType.magneteable;

                _mimicType.isBonus = _nextType.isBonus;

                _currentType = _nextType;

                _controller.AddBlock(_currentType, true);
            }
        }
    }
}