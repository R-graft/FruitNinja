using UnityEngine;

namespace winterStage
{
    public class CrushState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private RotateBlock _rotator;

        private Vector2 leftHalfDirection;

        private Vector2 rightHalfDirection;

        public CrushState(Block block)
        {
            _block = block;
            _mover = block.mover;
            _rotator = block.rotator;
        }

        public override void Enter()
        {
            _mover.SetDirection(_block.currentDirection);

            _block._slashView.transform.SetParent(null);

            _block._slashView.ActivateSplash();

            _rotator.SetRotateValue(50);
        }

        public override void Update()
        {
            _mover.ParabolaMove(_block.transform);

            _mover.MoveToTarget(Vector2.left, _block._leftSide);
            _mover.MoveToTarget(Vector2.right, _block._rightSide);

            _rotator.Rotate(_block._leftHalf);
            _rotator.Rotate(_block._rightHalf);
            _rotator.Rotate(_block._rightShadow);
            _rotator.Rotate(_block._leftShadow);
        }

        public override void Exit()
        {
            _block._slashView.transform.SetParent(_block.transform);

            _block._slashView.transform.position = _block.transform.position;
        }
    }
}
