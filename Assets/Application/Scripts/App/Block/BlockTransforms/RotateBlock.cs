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

        public void RotateToDirection(Transform transform, float direction)
        {
            transform.Rotate(Vector3.forward * direction * _rotateValue * Time.deltaTime);
        }
    }
}
