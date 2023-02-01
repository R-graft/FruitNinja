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

        private void Awake()
        {
            _creator = new CreateSystem();

            _creator.CreateBlocks(blocksList, this);
        }
        public Block GetBlock(string tag)
        {
            Block getted = _creator.Pools[tag].Get();

            return getted;
        }

        public void AddBlock(Block block)
        {
            _allBlocks.Add(block);
        }
    }
}
