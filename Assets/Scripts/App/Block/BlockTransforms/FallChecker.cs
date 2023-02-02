using UnityEngine;

namespace winterStage
{
    public class FallChecker : MonoBehaviour
    {
        private Block _block;

        private float fallValue;

        public FallChecker(Block block)
        {
            _block = block;
        }
        public void GetFallValue()
        {
            fallValue = Camera.main.ScreenPointToRay(Vector3.zero).origin.x;
        }

        public void CheckFall()
        {
            if (_block.transform.position.y < fallValue)
            {
                BlocksController.OnBlockFall.Invoke(_block);
            }
        }
    }
}
