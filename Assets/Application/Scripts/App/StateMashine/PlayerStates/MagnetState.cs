using UnityEngine;

namespace winterStage
{
    public class MagnetState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private Vector3 _magnetPos;

        private const float MoveSpeed = 4;

        public MagnetState(Block block, Vector3 magnetPos)
        {
            _block = block;
            _mover = block.mover;
            _magnetPos = magnetPos;
        }

        public override void Update()
        {
            _mover.MoveToTarget(_block.transform, _magnetPos, MoveSpeed);
        }

        public override void Exit()
        {
            _block.currentDirection = new Vector2(-_block.transform.position.x, -_block.transform.position.y * 2);
        }
    }
}
