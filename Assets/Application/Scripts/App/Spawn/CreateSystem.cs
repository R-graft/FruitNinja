using System.Collections.Generic;

namespace winterStage
{
    public class CreateSystem
    {
        public Dictionary<string, ObjectPool<Block>> Pools { get; private set; }

        public Dictionary<string, FactoryBlock<Block>> Factories { get; private set; }

        public Dictionary<string, BlockModel> Types_ { get; private set; }

        public void CreateBlocks(BlocksData _blocksList, BlocksController controller)
        {
            Pools = new Dictionary<string, ObjectPool<Block>>();

            Factories = new Dictionary<string, FactoryBlock<Block>>();

            Types_ = new Dictionary<string, BlockModel>();

            foreach (var type in _blocksList.blocksModels)
            {
                Types_.Add(type.tag, type);

                FactoryBlock<Block> factory = new FactoryBlock<Block>(type.blockType, controller.transform);

                Factories.Add(type.tag, factory);

                ObjectPool<Block> pool = new ObjectPool<Block>(() => PoolOnCreateNewBlock(type.tag), controller.PoolOnCreate, controller.PoolOnGet, controller.PoolOnDisable);

                for (int i = 0; i < type.poolCount; i++)
                {
                    var newObject = factory.CreateBlock(type);

                    pool.Add(newObject);

                    controller.AddBlock(newObject, false);
                }

                Pools.Add(type.tag, pool);
            }
        }
        public Block PoolOnCreateNewBlock(string tag)
        {
            return Factories[tag].CreateBlock(Types_[tag]);
        }
    }
}
