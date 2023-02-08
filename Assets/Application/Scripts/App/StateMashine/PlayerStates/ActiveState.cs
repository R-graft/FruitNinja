using UnityEngine;

namespace winterStage
{
    public class ActiveState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private RotateBlock _rotator;

        private ScaleBlock _scaler;

        public ActiveState(Block block, Vector2 moveDirection, float rotateValue, Vector3 scaleValue)
        {
            _block = block;
            _mover = block.mover;
            _rotator = block.rotator;
            _scaler= block.scaler;

            _block.currentDirection = moveDirection;
            _block.currentRotation = rotateValue;
            _block.currentScale = scaleValue;

        }
        public override void Enter()
        {
            _mover.SetStartDirection(_block.currentDirection);
            _rotator.SetRotateValue(_block.currentRotation);
            _scaler.SetStartScale(_block.transform, _block.currentScale);
        }

        public override void Update()
        {
            _mover.ParabolaMove(_block.transform);

            _rotator.Rotate(_block.halvesParent.transform);

            _scaler.RescaleBlock(_block.transform);
        }

        public override void Exit()
        {
            _block.currentDirection = _mover.GetDirection();
        }
    }
}
