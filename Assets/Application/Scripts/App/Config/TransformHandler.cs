using UnityEngine;

namespace winterStage
{
    public class TransformHandler: MonoBehaviour
    {
        [Header("Move multiplier")]
        public float _directionX;
        public float _directionY;

        public float maxForceMove = 1.8f;
        public float minForceMove = 1.7f;

        public float _forceModificator;

        [Header("Rotate multiplier")]
        public  float maxForceRotate = 200;
        public  float minForceRotate = 50;

        public  float maxDirectMultiplier = 2;
        public  float minDirectMultiplier = -1;

        [Header("Scale multiplier")]
        public  float minScale = 0.7f;
        public  float maxScale = 1.3f;

        private readonly (int left, int right) scaleDirection = (-1, 2);

        public Vector2 GetParabolaMoveDirection(Vector2 currentPosition)
        {
            _forceModificator = Random.Range(minForceMove, maxForceMove);

            _directionX = -currentPosition.x * _forceModificator;

            _directionY = -currentPosition.y * _forceModificator * 2;

            return new Vector2(_directionX, _directionY);
        }

        public float GetRandomRotateValue()
        {
            float rotateForce = Random.Range(minForceRotate, maxForceRotate);

            float rotateDirection = Random.Range(minDirectMultiplier, maxDirectMultiplier);

            return rotateForce * rotateDirection;
        }

        public Vector3 GetRandomScaleValue()
        {
            float startScaleValue = Random.Range(minScale, maxScale);

            int direction = Random.Range(scaleDirection.left, scaleDirection.right);

            return new Vector3(startScaleValue, startScaleValue, direction);
        }
    }
}
