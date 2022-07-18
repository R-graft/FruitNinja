using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField]
    private GameObject _trailPrefab;

    private Camera _camera;

    private GameObject _currentTrail;

    private float _minSlashDistance = 0.2f;

    private Vector2 _endPoint;

    public static Vector2 StartPoint { get; private set; }

    public static Queue<Vector2> Positions { get; private set; } 

    void Start()
    {
        Positions = new Queue<Vector2>();

        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCut();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCut();
            
        }
        if (Input.GetMouseButton(0))
        {
            Cutting();
        }
    }
    private void StartCut()
    {
        Positions.Clear();

        StartPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

        _currentTrail = Instantiate(_trailPrefab, transform);
    }
    private void StopCut()
    {
        _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

        Destroy(_currentTrail);

        if (Vector2.Distance(StartPoint, _endPoint) > _minSlashDistance)
        {
            GameEvents.slash.Invoke();
        }
    }
    private void Cutting()
    {
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);

        Positions.Enqueue(transform.position);
    }
}
