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

        public override T CreateObject()
        {
            var creatingBlock = Instantiate(_creatingObject, _creatingTransform);

            return creatingBlock;
        }
    }
}
    
