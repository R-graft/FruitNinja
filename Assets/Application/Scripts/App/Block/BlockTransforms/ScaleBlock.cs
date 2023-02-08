using UnityEngine;

namespace winterStage
{
    public class ScaleBlock
    {
        private float _maxScaleValue = 1.3f;
        private float _minScaleValue = 0.7f;

        private Vector3 _scaleStep = new Vector3(0.2f, 0.2f);

        public void RescaleBlock(Transform transform)
        {
            if (transform.localScale.x > _maxScaleValue || transform.localScale.x < _minScaleValue)
            {
                return;
            }
                transform.localScale += _scaleStep * Time.deltaTime;
        }

        public void SetStartScale(Transform transform, Vector3 scale)
        {
            transform.localScale = (Vector2)scale;

            _scaleStep *= scale.z;
        }
    }
}
