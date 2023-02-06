using UnityEngine;

namespace winterStage
{
    public class ScaleBlock
    {
        private float _startScaleValue;
        private float _endScaleValue;

        private const float MinScale = 0.7f;
        private const float MaxScale = 1.3f;

        private Vector3 _scaleStep = new Vector3(0.2f, 0.2f);


        public void RescaleBlock(Transform transform)
        {
            if (transform.localScale.x > MaxScale || transform.localScale.x < MinScale)
            {
                return;
            }
                transform.localScale += _scaleStep * Time.deltaTime;
        }

        public void GetScaleValue(Transform transform)
        {
            _startScaleValue = Random.Range(MinScale, MaxScale);

            _endScaleValue = Random.Range(MinScale, MaxScale);

            _scaleStep = _startScaleValue < _endScaleValue ? _scaleStep : -_scaleStep;

            transform.localScale = new Vector3(_startScaleValue, _startScaleValue, 0);
        }
    }
}
