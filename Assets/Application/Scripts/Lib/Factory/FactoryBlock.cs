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

            creatingBlock._slashView = Object.Instantiate(type.slashView, creatingBlock.transform);

            foreach (var renderer in creatingBlock._partsSprites)
            {
                renderer.sprite = type.sprite;
            }

            creatingBlock.Init();

            creatingBlock.StateMashine.Init(new DisableState(creatingBlock));

            return creatingBlock;
        }
    }
}
    
