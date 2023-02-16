using UnityEngine;

namespace winterStage
{
    public class MoveBlock 
    {
        private Vector3 _direction;

        private const float GravityStep = 10f;

        public static float _forceModificator = 1f;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public Vector3 GetDirection()
        {
            return _direction;
        }

        public void SetStartDirection(Vector2 direction)
        {
            if (direction.y <= 2)
            {
                _direction = new Vector2(direction.x, -direction.y/2);
            }

            else
            {
                _direction = direction;
            }
        }

        public void ParabolaMove(Transform transform)
        {
            transform.position += _direction * Time.deltaTime * _forceModificator;

            _direction = new Vector2(_direction.x, _direction.y -= GravityStep * Time.deltaTime * _forceModificator);
        }

        public void MoveToDirection(Vector3 direct, Transform transform)
        {
            transform.localPosition += direct * Time.deltaTime;
        }

        public void MoveToTarget(Transform transform, Vector3 target, float force)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, force * Time.deltaTime);
        }

        public static void SetForce(float force)
        {
            _forceModificator = force;
        }
    }
}
