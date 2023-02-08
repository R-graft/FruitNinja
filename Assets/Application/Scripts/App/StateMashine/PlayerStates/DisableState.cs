using UnityEngine;

namespace winterStage
{
    public class DisableState : State
    {
        public Block _block;

        private readonly Vector3 PartsOffset = new Vector3(0.3f, 0.4f, 0);

        public DisableState(Block block)
        {
            _block = block;
        }

        public override void Enter()
        {
            _block.transform.localScale = Vector3.one;

            _block.transform.localRotation = Quaternion.identity;

            _block.transform.localPosition = Vector3.zero;

            SetHalvesToDefault();
        }
        private void SetHalvesToDefault()
        {
            _block._leftSide.localPosition = Vector3.zero;
            _block._leftSide.localRotation = Quaternion.identity;
            _block._rightSide.localPosition = -PartsOffset;
            _block._rightSide.localRotation = Quaternion.identity;

            _block._leftHalf.localPosition = Vector3.zero;
            _block._leftHalf.localRotation = Quaternion.identity;
            _block._rightHalf.localPosition = PartsOffset;
            _block._rightHalf.localRotation= Quaternion.identity;

            _block._leftShadow.localPosition = -PartsOffset;
            _block._leftShadow.localRotation = Quaternion.identity;
            _block._rightShadow.localPosition = Vector3.zero;
            _block._rightShadow.localRotation = Quaternion.identity;
        }
    }
}
