using UnityEngine;

namespace winterStage
{
    public class MoveBlock 
    {
        private Vector3 _direction;

        private const float _force = 0.6f;

        private readonly Vector3 _gravityStep = new Vector2(0, 0.1f);

        public void SetStartDirection(Vector2 direction)
        {
            if (direction.y <= 0)
            {
                _direction = new Vector2(direction.x, -direction.y);
            }

            else
            {
                _direction = direction;
            }
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public Vector3 GetDirection()
        {
            return _direction;
        }

        public void ParabolaMove(Transform transform)
        {
            transform.position += _direction * _force * Time.deltaTime;

            _direction -= _gravityStep;
        }

        public void MoveToDirection(Vector3 target, Transform transform)
        {
            transform.localPosition += target * Time.deltaTime;
        }
    }
}
