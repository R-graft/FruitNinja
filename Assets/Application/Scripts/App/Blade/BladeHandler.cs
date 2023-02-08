using System;
using UnityEngine;

namespace winterStage
{
    public class BladeHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _trailPrefab;

        [SerializeField]private Camera _camera;

        private GameObject _currentTrail;

        private Vector2 _previousPosition;

        public const float MinSlashDistance = 0.002f;

        private bool _inputIsActive;

        public static Action<Vector3> OnBladeCuting;

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

            _currentTrail.SetActive(true);
        }
        private void StopCut()
        {
            _currentTrail.SetActive(false);
        }
        private void Cutting()
        {
            Vector2 _currentPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = _currentPosition;

            float speedCutting = (_currentPosition - _previousPosition).magnitude * Time.deltaTime;

            if (speedCutting > MinSlashDistance)
            {
                OnBladeCuting?.Invoke(_currentPosition);
            }
            _previousPosition = _currentPosition;
        }
    }
}

