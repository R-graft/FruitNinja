using System.Runtime.InteropServices;
using UnityEngine;

namespace winterStage
{
    public class ActiveState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private RotateBlock _rotator;

        private ScaleBlock _scaler;

        public ActiveState(Block block, Vector2 moveDirection)
        {
            _block = block;
            _mover = block.mover;
            _rotator = block.rotator;
            _scaler= block.scaler;

            block.currentDirection = moveDirection;
        }
        public override void Enter()
        {
            _mover.SetDirection(_block.currentDirection);

            _rotator.GetRandomRotateValue();

            _scaler.GetScaleValue(_block.transform);
        }

        public override void Update()
        {
            _mover.ParabolaMove(_block.transform);
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
