using UnityEngine;

namespace winterStage
{
    public class RotateBlock
    {
        private Transform _blockTransform;

        private float _rotateDirection;

        private float _rotateForce;

        private const float MaxForceMultiplier = 10;
        private const float MinForceMultiplier = 1;

        private const float MaxDirectMultiplier = 2;
        private const float MinDirectMultiplier = -1;

        public RotateBlock(Transform transform)
        {
            _blockTransform = transform;
        }

        public void GetRotateValue()
        {
            _rotateForce = Random.Range(MinForceMultiplier, MaxForceMultiplier);

            _rotateDirection = Random.Range(MinDirectMultiplier, MaxDirectMultiplier);
        }
        public void Rotate()
        {
            _blockTransform.Rotate(Vector3.forward * _rotateForce * _rotateDirection);
        }

        public void StopRotate()
        {
            _rotateForce = 0;

            _rotateDirection = 0;
        }
    }
}
