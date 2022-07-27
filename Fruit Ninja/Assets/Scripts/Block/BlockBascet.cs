using UnityEngine;

public class BlockBascet : MonoBehaviour, IBlock
{
    [SerializeField]
    private FlyingSimulation _flyingSimulation;

    [SerializeField]
    private BlockRotateAndScale _rotateAndScale;

    [SerializeField]
    private GameObject _block;

    [SerializeField]
    private GameObject _blockShadow;

    private float _slashDistance = 1f;

    private bool isSlashed = false;

    private void OnEnable()
    {
        BlockMove();

        isSlashed = false;
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

        isSlashed = true;

        GameEvents.BascetBlockSlashed.Invoke();
    }

    public void BlockMove()
    {
        _block.SetActive(true);

        _blockShadow.SetActive(true);

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
    public void MoveMagnet(Vector2 magnetPos, bool isMove)
    {
        if (isMove)
        {
            _flyingSimulation.magnetPos = magnetPos;

            _flyingSimulation.magnetMove = true;
        }
        else
        {
            _flyingSimulation.magnetMove = false;
        }
    }
}
