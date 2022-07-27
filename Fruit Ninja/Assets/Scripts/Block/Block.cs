using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
    [SerializeField]
    private FlyingSimulation _flyingSimulation;

    [SerializeField]
    private BlockRotateAndScale _rotateAndScale;

    [SerializeField]
    private GameObject _block;

    [SerializeField]
    private GameObject _blockShadow;

    [SerializeField]
    private GameObject _halves;

    private float _slashDistance = 1f;

    private bool isSlashed = false;

    private void OnEnable()
    {
        BlockMove();

        isSlashed = false;
    }
    private void OnDisable()
    {
        if (_block.activeSelf && transform.position.y < -5.5f)
        {
            GameEvents.fruitLost.Invoke();
        }
    }
    public void CheckSlash(Vector2 _bladePos)
    {
        if (gameObject.activeSelf && !isSlashed)
        {
            if (Vector2.Distance(transform.position, _bladePos) < _slashDistance)
            {
                BlockSlashed();

                isSlashed = true;
            }
        }
    }
    public void BlockSlashed()
    {
        _block.SetActive(false);

        _blockShadow.SetActive(false);

        _halves.SetActive(true);

        isSlashed = true;

        GameEvents.fruitSlashed.Invoke();
    }
    
    public void BlockMove()
    {
        _block.SetActive(true);

        _blockShadow.SetActive(true);

        _halves.SetActive(false);

        _rotateAndScale.StartRotateAndSale();

        _flyingSimulation.MoveDirection();
    }
    public void BombSlash(Vector3 bombPos)
    {
        if (gameObject.activeSelf)
        {
            _flyingSimulation.BombBlow(bombPos);
        }
    }
    public void SetIceSpeed(string speedMode)
    {
        _flyingSimulation.ActivateIceSpeed(speedMode);
    }
}
