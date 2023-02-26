using System;
using UnityEngine;

namespace winterStage
{
    public class CutHandler
    {
        private TrailRenderer _currentTrail;

        private Camera _mCamera;

        private Vector2 _previousPosition;

        private Vector2 _currentPosition;

        private float _minSlashSpeed = 25;

        private BladeInfo _currentSlash = new BladeInfo();

        public static Action<BladeInfo> OnBladeCuting;

        public CutHandler(TrailRenderer trail, Camera camera, float minslashSpeed)
        {
            _currentTrail = trail;

            _mCamera = camera;

            _minSlashSpeed = minslashSpeed;
        }
        public void StartCut(Vector2 position)
        {
            _previousPosition = _mCamera.ScreenToWorldPoint(position); ;
        }
        public void StopCut()
        {
            _currentTrail.gameObject.SetActive(false);
        }
        public void Cutting(Vector2 currentPosinion)
        {
            _currentPosition = _mCamera.ScreenToWorldPoint(currentPosinion);

            _currentTrail.gameObject.transform.position = _currentPosition;

            _currentTrail.gameObject.SetActive(true);

            float speedCutting = (_currentPosition - _previousPosition).magnitude / Time.deltaTime;

            if (speedCutting > _minSlashSpeed)
            {
                _currentSlash.currentDirection = _previousPosition - _currentPosition;

                _currentSlash.currentPosition = _currentPosition;

                OnBladeCuting?.Invoke(_currentSlash);
            }
            _previousPosition = _currentPosition;
        }
    }

    public struct BladeInfo
    {
        public Vector2 currentPosition;

        public Vector2 currentDirection;
    }
}

