using UnityEngine;

namespace winterStage
{
    public class CrushState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private RotateBlock _rotator;

        public CrushState(Block block)
        {
            _block = block;
            _mover = block.mover;
            _rotator = block.rotator;
        }

        public override void Enter()
        {
            _mover.SetDirection(_block.currentDirection);

            _rotator.GetRandomRotateValue();
        }

        public override void Update()
        {
            _mover.ParabolaMove(_block.transform);

            _mover.MoveToDirection(Vector3.left, _block._leftSide.transform);
            _mover.MoveToDirection(Vector3.right, _block._rightSide.transform);

            _rotator.Rotate(_block._leftSide.transform);
            _rotator.Rotate(_block._rightSide.transform);
        }
    }
}
