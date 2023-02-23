using UnityEngine;

namespace winterStage
{
    public class BladeHandler : MonoBehaviour
    {
        [Header("Config")]
        public float minSlashSpeed = 20;

        [Header("Components")]
        [SerializeField] private TrailRenderer _trailPrefab;

        [SerializeField] private Camera _camera;

        private TrailRenderer _firstTrail;

        private CutHandler _firstCutter;

        private bool _inputIsActive;
        public void Init()
        {
            _firstTrail = Instantiate(_trailPrefab, transform);

            _firstCutter = new CutHandler(_firstTrail, _camera, minSlashSpeed);

            EnableBlade();

            if (Application.platform == RuntimePlatform.Android)
            {
                minSlashSpeed = 15;
            }
        }

        public void EnableBlade()
        {
            _inputIsActive = true;
        }
        public void DisableBlade()
        {
            _inputIsActive = false;

            _firstTrail.gameObject.SetActive(false);
        }

        
        private void GetInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.touchCount > 1)
                {
                    var firstTouch = Input.GetTouch(0);

                    _firstCutter.StartCut(firstTouch.position);
                }

                else
                {
                    _firstCutter.StartCut(Input.mousePosition);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (Input.touchCount > 1)
                {
                    var firstTouch = Input.GetTouch(0);

                    _firstCutter.Cutting(firstTouch.position);
                }
                else
                {
                   _firstCutter.Cutting(Input.mousePosition);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _firstCutter.StopCut();
            }
        }

        void Update()
        {
            if (_inputIsActive)
            {
                GetInputs();
            }
        }

    }    
}

