using UnityEngine;
using System.Collections.Generic;

namespace winterStage
{
    public class CreateSystem
    {
        public Dictionary<string, ObjectPool<Block>> Pools { get; private set; }

        public Dictionary<string, AbstractFactory<Block>> Factories { get; private set; }

        public void CreateBlocks(BlocksList _blocksList, BlocksController controller)
        {
            Pools = new Dictionary<string, ObjectPool<Block>>();

            Factories = new Dictionary<string, AbstractFactory<Block>>();

            foreach (var type in _blocksList.blocksTypes)
            {
                AbstractFactory<Block> factory = new FactoryBlock<Block>(type.blockType, controller.transform);

                Factories.Add(type.tag, factory);

                ObjectPool<Block> pool = new ObjectPool<Block>(() => PoolOnCreateNewBlock(type.blockType), controller.PoolOnCreate, controller.PoolOnGet, controller.PoolOnDisable);

                for (int i = 0; i < type.poolCount; i++)
                {
                    var newObject = factory.CreateObject();

                    pool.Add(newObject);

                    controller.AddBlock(newObject);

                    newObject.tag = type.tag;
                }

                Pools.Add(type.tag, pool);
            }
        }
        public Block PoolOnCreateNewBlock(Block block)
        {
            return Factories[block.tag].CreateObject();
        }
    }
}
