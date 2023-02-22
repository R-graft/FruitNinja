using UnityEngine;

namespace winterStage
{
    public class ScaleBlock
    {
        private const float _scaleStep = 0.3f;

        private Vector3 _startScale;

        private Vector3 _endScale;

        public void RescaleBlock(Transform transform)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _endScale, _scaleStep * Time.deltaTime);
        }

        public void SetStartScale(Transform transform, Vector2 scale)
        {
            _startScale = new Vector3(scale.x, scale.x, scale.x);

            _endScale = new Vector3(scale.y, scale.y, scale.y);

            transform.localScale = _startScale;
        }
    }
}
