using UnityEngine;

namespace winterStage
{
    public class TransformHandler: MonoBehaviour
    {
        [Header("Move multiplier")]
        public float _forceModificator = 1;

        [Header("Rotate multiplier")]
        public  float maxForceRotate = 200;
        public  float minForceRotate = 50;

        public  float maxDirectMultiplier = 2;
        public  float minDirectMultiplier = -1;

        [Header("Scale multiplier")]
        public  float minScale = 0.85f;
        public  float maxScale = 1.3f;

        public Vector2 GetParabolaMoveDirection(Vector2 currentPosition)
        {
            var directionX = -currentPosition.x * _forceModificator;

            var directionY = -currentPosition.y * _forceModificator * 2;

            return new Vector2(directionX, directionY);
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

            float endDsaleValue = Random.Range(minScale, maxScale);

            return new Vector2(startScaleValue, endDsaleValue);
        }
    }
}
