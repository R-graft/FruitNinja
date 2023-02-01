using UnityEngine;

namespace winterStage
{
    public class ScaleBlock
    {
        private Transform _blockTransform;

        private float _startScaleValue;
        private float _endScaleValue;

        private const float MinStartScale = 1f;
        private const float MaxStartScale = 2.5f;

        private const float ScaleTime = 3;

        private float _currentTime;

        private Vector3 ScaleStep = new Vector3(0.005f, 0.005f);

        public ScaleBlock(Transform transform)
        {
            _blockTransform = transform;
        }

        public void RescaleBlock()
        {
            if (_currentTime > 0)
            {
                _blockTransform.localScale += ScaleStep;

                _currentTime -= Time.fixedDeltaTime;
            }
        }

        public void GetScaleValue()
        {
            _startScaleValue = Random.Range(MinStartScale, MaxStartScale);

            _endScaleValue = Random.Range(MinStartScale, MaxStartScale);

            ScaleStep = _startScaleValue < _endScaleValue ? ScaleStep : -ScaleStep;

            _blockTransform.localScale = new Vector3(_startScaleValue, _startScaleValue, 0);

            _currentTime = ScaleTime;
        }
    }
}
