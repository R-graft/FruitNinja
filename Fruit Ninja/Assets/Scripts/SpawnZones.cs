using UnityEngine;

public class SpawnZones : MonoBehaviour
{
    [SerializeField]
    private Transform _pointOne;

    [SerializeField]
    private Transform _pointTwo;

    [SerializeField]
    private Transform _pointThree;

    [SerializeField]
    private Transform _pointFour;

    [SerializeField]
    private Transform _pointFive;

    [SerializeField]
    private Transform _pointSex;

    [SerializeField]
    private Transform _pointSeven;

    public Vector2 _leftUp { get; private set; }

    public Vector2 _leftDown { get; private set; }

    public Vector2 _downLeft { get; private set; }

    public Vector2 _downRight { get; private set; }

    public Vector2 _rightDown { get; private set; }

    public Vector2 _rightUp { get; private set; }

    void Start()
    {
        InitializeSpawnZones();
    }

    private void InitializeSpawnZones()
    {
        _leftUp = new Vector2(_pointOne.position.x, Random.Range(_pointOne.position.y, _pointTwo.position.y));

        _leftDown = new Vector2(_pointTwo.position.x, Random.Range(_pointTwo.position.y, _pointThree.position.y));

        _downLeft = new Vector2(Random.Range(_pointThree.position.x, _pointFour.position.x), _pointThree.position.y);

        _downRight = new Vector2(Random.Range(_pointFour.position.x, _pointFive.position.x), _pointFour.position.y);

        _rightDown = new Vector2(_pointFive.position.x, Random.Range(_pointFive.position.y, _pointSex.position.y));

        _rightUp = new Vector2(_pointSex.position.x, Random.Range(_pointSex.position.y, _pointSeven.position.y));
    }
}
