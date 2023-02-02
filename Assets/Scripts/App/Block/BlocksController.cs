using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BlocksController : MonoBehaviour
    {
        public  BlocksList blocksList;

        private CreateSystem _creator;

        private HashSet<Block> _activeBlocks = new HashSet<Block>();

        private List<Block> _allBlocks = new List<Block>();

        private Block _currentBlock;

        private void Awake()
        {
            _creator = new CreateSystem();

            _creator.CreateBlocks(blocksList, this);

        }
        public void ActivateBlock(string tag, Vector2 position)
        {
            _currentBlock = _creator.Pools[tag].Get();

            _currentBlock.transform.position = position;

            _currentBlock.Init();

            _currentBlock.StateMashine.SetState(new MoveState(_currentBlock));

            _activeBlocks.Add(_currentBlock);
        }

        public void DeactivateBlock()
        {

        }
        public void AddBlock(Block block)
        {
            _allBlocks.Add(block);
        }

        public void PoolOnGet(Block block)
        {
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
    }
}
