namespace winterStage
{
    public class FactoryBlock<T> : AbstractFactory<T> where T : Block
    {
        private T _creatingObject;

        private BlocksController _blocksController;
        public FactoryBlock(T currentBlockType, BlocksController controller)
        {
            _creatingObject = currentBlockType;

            _blocksController = controller;
        }

        public override T CreateObject()
        {
            var creatingBlock = Instantiate(_creatingObject, _blocksController.transform);

            return creatingBlock;
        }
    }
}
    
