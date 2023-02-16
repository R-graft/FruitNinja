using UnityEngine;

namespace winterStage
{
    public class CrushState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private Vector3 _slashDirection;

        public CrushState(Block block, Vector2 slashDirection)
        {
            _block = block;
            _mover = block.mover;
            _slashDirection = slashDirection;
        }

        public override void Enter()
        {
            _mover.SetDirection(_block.currentDirection);

            _block.slashView.transform.SetParent(null);

            _block.slashView.ActivateSplash();

            _block.SlashInBehaviour();
        }

        public override void Update()
        {
            _mover.ParabolaMove(_block.transform);
            _mover.MoveToDirection(-_slashDirection.normalized * 3, _block.transform);

            _block.SlashUpdateBehaviour();
        }

        public override void Exit()
        {
            _block.slashView.transform.SetParent(_block.transform);

            _block.slashView.transform.position = _block.transform.position;
        }
    }
}
