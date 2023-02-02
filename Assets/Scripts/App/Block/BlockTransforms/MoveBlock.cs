using UnityEngine;

namespace winterStage
{
    public class MoveBlock 
    {
        private Transform _blockTransform;

        private float _launchForceX;
        private float _launchForceY;

        private float _forceModificator;

        private const float MaxForceMultiplier = 2;
        private const float MinForceMultiplier = 1.5f;

        private float _gravityValue;
        private const float _gravityStep = 0.05f;

        public MoveBlock(Transform transform)
        {
            _blockTransform = transform;
        }

        public void ParabolaMove()
        {
            _blockTransform.position += new Vector3((_launchForceX), _launchForceY + _gravityValue) * Time.deltaTime;

            _gravityValue -= _gravityStep;
        }

        public void GetParabolaMoveDirections()
        {
            _forceModificator = Random.Range(MinForceMultiplier, MaxForceMultiplier);

            _launchForceX = -_blockTransform.position.x;

            _launchForceY = -_blockTransform.position.y * 2;

            _gravityValue = 0;
        }

        public void MoveToTarget(Vector3 target)
        {
            _blockTransform.position += target * Time.deltaTime;
        }
    }
}
