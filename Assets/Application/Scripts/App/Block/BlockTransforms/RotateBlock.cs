using DG.Tweening;
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

        public void SimpleRotate(Transform transform)
        {
            transform.Rotate(Vector3.forward * _rotateValue * Time.deltaTime);
        }

        public void FullRotate(Transform transform)
        {
            transform.Rotate(new Vector3(1,1,1) * _rotateValue * Time.deltaTime);
        }

        public void RotateToDirection(Transform transform, float direction)
        {
            transform.Rotate(Vector3.forward * direction * _rotateValue * Time.deltaTime);
        }

        public void FullRotateToDirection(Transform transform, float direction)
        {
            transform.Rotate(new Vector3(1, 1, direction)* _rotateValue * Time.deltaTime);
        }
    }
}
