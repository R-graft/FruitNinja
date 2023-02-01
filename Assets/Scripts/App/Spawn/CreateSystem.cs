using UnityEngine;
using System.Collections.Generic;

namespace winterStage
{
    public class CreateSystem : MonoBehaviour
    {
        public Dictionary<string, ObjectPool<Block>> Pools { get; private set; }

        public Dictionary<string, AbstractFactory<Block>> Factories { get; private set; }

        public void SpawnBlocks(BlocksList _blocksList, BlocksController controller)
        {
            Pools = new Dictionary<string, ObjectPool<Block>>();

            Factories = new Dictionary<string, AbstractFactory<Block>>();

            foreach (var type in _blocksList.blocksTypes)
            {
                AbstractFactory<Block> factory = new FactoryBlock<Block>(type.blockType, controller);

                Factories.Add(type.blockType.blockTag, factory);

                ObjectPool<Block> pool = new ObjectPool<Block>(() => PoolOnCreateNewBlock(type.blockType), type.blockType.PoolOnCreate, type.blockType.PoolOnGet, type.blockType.PoolOnDisable);

                for (int i = 0; i < type.poolCount; i++)
                {
                    var newObject = factory.CreateObject();

                    pool.Add(newObject);
                }

                Pools.Add(type.blockType.blockTag, pool);
            }
        }
        public Block PoolOnCreateNewBlock(Block block)
        {
            return Factories[block.tag].CreateObject();
        }
    }
}
