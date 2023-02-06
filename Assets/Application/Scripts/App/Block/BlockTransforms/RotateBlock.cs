using UnityEngine;

namespace winterStage
{
    public class RotateBlock
    {
        private float _rotateDirection;

        private float _rotateForce;

        private const float MaxForceMultiplier = 200;
        private const float MinForceMultiplier = 50;

        private const float MaxDirectMultiplier = 2;
        private const float MinDirectMultiplier = -1;

        public void GetRandomRotateValue()
        {
            _rotateForce = Random.Range(MinForceMultiplier, MaxForceMultiplier);

            _rotateDirection = Random.Range(MinDirectMultiplier, MaxDirectMultiplier);
        }

        public void SetRotateValue(float force, float direction)
        {
            _rotateForce = force;

            _rotateDirection= direction;
        }

        public void Rotate(Transform transform)
        {
            transform.Rotate(Vector3.forward * _rotateDirection * _rotateForce * Time.deltaTime);
        }

        public void SetAcces(bool assesValue)
        {
            
        }
    }
}
