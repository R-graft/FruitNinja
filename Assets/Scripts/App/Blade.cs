using System;
using UnityEngine;

namespace winterStage
{
    public class Blade : MonoBehaviour
    {
        public SpriteRenderer renderer;
        [SerializeField]
        private GameObject _trailPrefab;

        private GameObject _currentTrail;

        private Camera _camera;

        private Vector2 _previousPosition;

        public float _minSlashDistance = 0.002f;

        private bool _inputIsActive;

        public static Action<Vector2> OnBladeCuting;

        private void Awake()
        {
            Init();
        }
        public void Init()
        {
            _camera = Camera.main;

            _currentTrail ??= Instantiate(_trailPrefab, transform);

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
            transform.position = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);

            Vector2 _currentPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            float speedCutting = (_currentPosition - _previousPosition).magnitude * Time.deltaTime;

            if (speedCutting > _minSlashDistance)
            {
                OnBladeCuting?.Invoke(_currentPosition);
            }
            _previousPosition = _currentPosition;
        }
    }
}

