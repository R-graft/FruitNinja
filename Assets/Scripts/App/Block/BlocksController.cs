using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BlocksController : MonoBehaviour
    {
        public  BlocksList blocksList;

        private CreateSystem _creator;

        private List<Block> _activeBlocks;

        public Block GetBlock(string tag)
        {
            return _creator.Pools[tag].Get();
        }
    }
}
