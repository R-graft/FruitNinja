using System;
using UnityEngine;

namespace winterStage
{
    public class BladeHandler : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailPrefab;

        [SerializeField] private Camera _camera;

        private TrailRenderer _currentTrail;

        private BladeInfo _currentSlash = new BladeInfo();

        private Vector2 _previousPosition;

        public float MinSlashDistance = 0.002f;

        private bool _inputIsActive;

        public static Action<BladeInfo> OnBladeCuting;

        public void Init()
        {
            _currentTrail = Instantiate(_trailPrefab, transform);

            EnableBlade();
        }
        public void EnableBlade()
        {
            _inputIsActive = true;
        }
        public void DisableBlade()
        {
            _inputIsActive = false;
        }

        private void InputController()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCut();
            }

            if (Input.GetMouseButton(0))
            {
                Cutting();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopCut();
            }
        }
        void Update()
        {
            if (_inputIsActive)
            {
                InputController();
            }
        }

        private void StartCut()
        {
            _previousPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            
        }
        private void StopCut()
        {
            _currentTrail.gameObject.SetActive(false);
        }
        private void Cutting()
        {
            Vector2 _currentPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

#if PLATFORM_ANDROID
            if (Input.touchCount > 1)
            {
                return;
            }
#endif
            transform.position = _currentPosition;

            float speedCutting = (_currentPosition - _previousPosition).magnitude * Time.deltaTime;

            if (speedCutting > MinSlashDistance)
            {
                _currentTrail.gameObject.SetActive(true);
                _currentSlash.currentDirection = _previousPosition - _currentPosition;

                _currentSlash.currentPosition = _currentPosition;

                OnBladeCuting?.Invoke(_currentSlash);
            }
            _previousPosition = _currentPosition;
        }
    }

    public struct BladeInfo
    {
        public Vector3 currentPosition;

        public Vector2 currentDirection;
    }
}

