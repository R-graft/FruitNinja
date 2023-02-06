using UnityEngine;

namespace winterStage
{
    public class DirectionHandler
    {
        private float _directionX;
        private float _directionY;

        private const float MaxForceMultiplier = 1.8f;
        private const float MinForceMultiplier = 1.7f;

        private float _forceModificator;

        public Vector2 GetParabolaMoveDirection(Vector2 currentPosition)
        {
            _forceModificator = Random.Range(MinForceMultiplier, MaxForceMultiplier);

            _directionX = -currentPosition.x * _forceModificator;

            _directionY = -currentPosition.y * _forceModificator * 2;

            return new Vector2(_directionX, _directionY);
        }
    }
}
