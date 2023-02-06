using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BlocksController : MonoBehaviour
    {
        public  BlocksList blocksList;

        private CreateSystem _creator;

        private HashSet<Block> _activeBlocks = new HashSet<Block>();

        private HashSet<Block> _slashedBlocks = new HashSet<Block>();

        private List<Block> _allBlocks = new List<Block>();

        private const float SlashRadius = 1;

        private float _deadPoint;

        private const float DeadZoneOffset = 3;
        private const float CheckFallTime = 2f;

        public void Init()
        {
            _creator = new CreateSystem();

            _creator.CreateBlocks(blocksList, this);

            _deadPoint = ScreenSizeHandler.downScreenEdge - DeadZoneOffset;
            print(_deadPoint);
            StartCoroutine(CheckFall());
        }
        public Block GetBlock(string tag, Vector2 position)
        {
            var currentBlock = _creator.Pools[tag].Get(position);

            _activeBlocks.Add(currentBlock);

            return currentBlock;
        }

        public void DeactivateBlock(Block block)
        {
            _creator.Pools[block.tag].Disable(block);

            block.StateMashine.SetState(new DisableState(block.transform));
        }
        public void AddBlock(Block block)
        {
            _allBlocks.Add(block);
        }

        private void CheckSlash(Vector2 bladePos)
        {
            foreach (var block in _activeBlocks)
            {
                var currentDistance = (bladePos - (Vector2)block.transform.position).sqrMagnitude;

                if (currentDistance <= SlashRadius)
                {
                    block.StateMashine.SetState(new CrushState(block));

                    _activeBlocks.Remove(block);

                    _slashedBlocks.Add(block);

                    CheckSlash(bladePos);

                    break;
                }
            }
        }

        private void GetFallBlocks()
        {
            if (_activeBlocks.Count > 0)
            {
                foreach (var block in _activeBlocks)
                {
                    if (block.transform.position.y < _deadPoint)
                    {
                        DeactivateBlock(block);
                    }
                }

                foreach (var block in _slashedBlocks)
                {
                    if (block.transform.position.y < _deadPoint)
                    {
                        DeactivateBlock(block);
                    }
                }
            }
        }
        private IEnumerator CheckFall()
        {
            while (true)
            {
                yield return new WaitForSeconds(CheckFallTime);

                GetFallBlocks();
            }
        }
#region(poolFunctions)
        public void PoolOnGet(Block block, Vector2 newPosition)
        {
            block.transform.position = newPosition;

            block.gameObject.SetActive(true);
        }

        public void PoolOnCreate(Block block)
        {
            block.gameObject.SetActive(false);
        }

        public void PoolOnDisable(Block block)
        {
            block.gameObject.SetActive(false);
        }
#endregion

        private void OnEnable()
        {
            BladeHandler.OnBladeCuting += CheckSlash;
        }

        private void OnDisable()
        {
            BladeHandler.OnBladeCuting -= CheckSlash;
        }
    }
}
