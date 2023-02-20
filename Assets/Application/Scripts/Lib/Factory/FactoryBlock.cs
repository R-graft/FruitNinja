using UnityEngine;

namespace winterStage
{
    public class FactoryBlock<T> : AbstractFactory<T> where T : Block
    {
        private T _creatingObject;

        private Transform _creatingTransform;
        public FactoryBlock(T currentBlockType, Transform transform)
        {
            _creatingObject = currentBlockType;

            _creatingTransform = transform;
        }

        public T CreateBlock(BlockModel type)
        {
            var creatingBlock = Object.Instantiate(_creatingObject, _creatingTransform);

            creatingBlock.blockTag = type.tag;

            creatingBlock.slashView = Object.Instantiate(type.slashView, creatingBlock.transform);

            creatingBlock.isBonus = type.isBoost;

            foreach (var renderer in creatingBlock.partsRenderers)
            {
                renderer.sprite = type.sprite;
            }

            creatingBlock.Init();

            creatingBlock.StateMashine.Init(new DisableState(creatingBlock));

            return creatingBlock;
        }
    }
}
    
