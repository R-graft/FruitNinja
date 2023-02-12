using UnityEngine;

namespace winterStage
{
    public class CrushState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private RotateBlock _rotator;

        private Vector3 _slashDirection;

        public CrushState(Block block, Vector2 slashDirection)
        {
            _block = block;
            _mover = block.mover;
            _rotator = block.rotator;
            _slashDirection= slashDirection;
        }

        public override void Enter()
        {
            _mover.SetDirection(_block.currentDirection);

            _block._slashView.transform.SetParent(null);

            _block._slashView.ActivateSplash();
        }

        public override void Update()
        {
            _mover.ParabolaMove(_block.transform);
            _mover.MoveToTarget(-_slashDirection.normalized * 3, _block.transform);

            _mover.MoveToTarget(Vector2.left * 2, _block._leftSide);
            _mover.MoveToTarget(Vector2.right * 2, _block._rightSide);

            _rotator.RotateToDirection(_block._leftHalf, -1);
            _rotator.RotateToDirection(_block._rightHalf, 1);
            _rotator.RotateToDirection(_block._rightShadow,1);
            _rotator.RotateToDirection(_block._leftShadow,-1);
        }

        public override void Exit()
        {
            _block._slashView.transform.SetParent(_block.transform);

            _block._slashView.transform.position = _block.transform.position;
        }
    }
}
