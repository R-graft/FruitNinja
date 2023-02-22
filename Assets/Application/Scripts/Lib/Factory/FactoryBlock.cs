using UnityEngine;

namespace winterStage
{
    public class FactoryBlock<T> where T : Block
    {
        private T _creatingObject;

        private BlocksController _controller;

        private BonusController _bonus;
        public FactoryBlock(T currentBlockType, BlocksController controller, BonusController bonus)
        {
            _creatingObject = currentBlockType;

            _controller = controller;

            _bonus = bonus;
        }

        public T CreateBlock(BlockModel type)
        {
            var creatingBlock = Object.Instantiate(_creatingObject, _controller.transform);

            creatingBlock.blockTag = type.tag;

            creatingBlock.slashView = Object.Instantiate(type.slashView, creatingBlock.transform);

            creatingBlock.isBonus = type.isBoost;

            if (type.isBoost)
            {
                if (creatingBlock.TryGetComponent(out IBoostBlock boost))
                {
                    boost.BonusController = _bonus;
                }
            }

            if (!creatingBlock.is3d)
            {
                foreach (var renderer in creatingBlock.partsRenderers)
                {
                    renderer.sprite = type.sprite;
                }
            }

            creatingBlock.Init();

            creatingBlock.StateMashine.Init(new DisableState(creatingBlock));

            _controller.AddBlock(creatingBlock, false);

            return creatingBlock;
        }
    }
}
    
