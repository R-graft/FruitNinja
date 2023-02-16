using UnityEngine;

namespace winterStage
{
    public class DisableState : State
    {
        public Block _block;

        public DisableState(Block block)
        {
            _block = block;
        }

        public override void Enter()
        {
            _block.SetDefaultTransform();
        }
    }
}
