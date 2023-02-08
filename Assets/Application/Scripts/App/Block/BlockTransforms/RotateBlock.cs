using UnityEngine;

namespace winterStage
{
    public class RotateBlock
    {
        private float _rotateValue;

        public void SetRotateValue(float value)
        {
            _rotateValue = value;
        }

        public void Rotate(Transform transform)
        {
            transform.Rotate(Vector3.forward * _rotateValue * Time.deltaTime);
        }
    }
}
