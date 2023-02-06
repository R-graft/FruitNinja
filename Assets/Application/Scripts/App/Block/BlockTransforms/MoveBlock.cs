using UnityEngine;

namespace winterStage
{
    public class MoveBlock 
    {
        private Vector3 _direction;

        private float _force = 0.4f;

        private readonly Vector3 _gravityStep = new Vector2(0, 0.1f);

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
