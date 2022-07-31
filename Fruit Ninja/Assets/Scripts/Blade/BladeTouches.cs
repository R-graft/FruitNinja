using UnityEngine;

public class BladeTouches : MonoBehaviour
{
    [SerializeField]
    private SlashController _collisionManager;

    [SerializeField]
    private GameObject _trailPrefab;

    [SerializeField]
    private GameObject _trailOnePrefab;

    private Camera _camera;

    private GameObject _currentTrail;

    private GameObject _currentTrailOne;

    void Awake()
    {
        _camera = Camera.main;

        _currentTrail = Instantiate(_trailPrefab);

        _currentTrail.SetActive(false);

        _currentTrailOne = Instantiate(_trailOnePrefab);

        _currentTrailOne.SetActive(false);

        GameEvents.gameOver.AddListener(DestroyTrail);
    }
    void Update()
    {
        int nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {
            for (int i = 0; i < nbTouches; i++)
            {
                if (i == 0)
                {
                    Touch touch0 = Input.GetTouch(i);

                    TouchPhase phase0 = touch0.phase;

                    switch (phase0)
                    {
                        case TouchPhase.Began:

                            StartCut(_currentTrail);

                            break;

                        case TouchPhase.Moved:

                            Cutting(_currentTrail, touch0.position);

                            break;

                        case TouchPhase.Ended:

                            StopCut(_currentTrail);

                            break;
                    }
                }
                else
                {
                    Touch touch1 = Input.GetTouch(i);

                    TouchPhase phase1 = touch1.phase;

                    switch (phase1)
                    {
                        case TouchPhase.Began:

                            StartCut(_currentTrailOne);

                            break;

                        case TouchPhase.Moved:

                            Cutting(_currentTrailOne, touch1.position);

                            break;

                        case TouchPhase.Ended:

                            StopCut(_currentTrailOne);

                            break;
                    }
                }
            }
        }
    }
    private void StartCut(GameObject prefab)
    {
        prefab.SetActive(true);
    }
    private void StopCut(GameObject prefab)
    {
        prefab.SetActive(false);
    }
    private void Cutting(GameObject prefab, Vector2 touchPos)
    {
        Vector2 _currentPosition = _camera.ScreenToWorldPoint(touchPos);

        prefab.transform.position = _currentPosition;

       _collisionManager.CheckSlash(_currentPosition); 
    }
    private void DestroyTrail()
    {
        Destroy(_currentTrail);

        Destroy(_currentTrailOne);
    }
}
