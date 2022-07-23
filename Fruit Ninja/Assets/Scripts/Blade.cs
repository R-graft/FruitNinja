using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField]
    private GameObject _trailPrefab;

    [SerializeField]
    private CollisionManager _collisionManager;

    private Camera _camera;

    private GameObject _currentTrail;

    private Vector2 _previousPosition;

    public float _minSlashDistance = 0.002f;

    void Start()
    {
        _camera = Camera.main;

        _currentTrail = Instantiate(_trailPrefab, transform);
    }

    void Update()
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
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 _currentPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        float speedCutting = (_currentPosition -_previousPosition).magnitude * Time.deltaTime;

        if (speedCutting > _minSlashDistance)
        {
           _collisionManager.CheckSlash(_currentPosition);
        }
        _previousPosition = _currentPosition;
    }
}
