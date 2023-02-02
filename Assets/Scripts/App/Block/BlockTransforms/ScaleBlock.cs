using UnityEngine;

namespace winterStage
{
    public class ScaleBlock
    {
        private Transform _blockTransform;

        private float _startScaleValue;
        private float _endScaleValue;

        private const float MinScale = 0.7f;
        private const float MaxScale = 1.3f;

        private Vector3 _scaleStep = new Vector3(0.2f, 0.2f);

        public ScaleBlock(Transform transform)
        {
            _blockTransform = transform;
        }

        public void RescaleBlock()
        {
            if (_blockTransform.localScale.x > MaxScale || _blockTransform.localScale.x < MinScale)
            {
                return;
            }
                _blockTransform.localScale += _scaleStep * Time.deltaTime;
        }

        public void GetScaleValue()
        {
            _startScaleValue = Random.Range(MinScale, MaxScale);

            _endScaleValue = Random.Range(MinScale, MaxScale);

            _scaleStep = _startScaleValue < _endScaleValue ? _scaleStep : -_scaleStep;

            _blockTransform.localScale = new Vector3(_startScaleValue, _startScaleValue, 0);
        }
    }
}
