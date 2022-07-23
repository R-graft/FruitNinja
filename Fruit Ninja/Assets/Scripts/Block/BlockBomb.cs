using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBomb : MonoBehaviour, IBlock
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

    public bool isSlashed;

    public void CheckSlash(Vector2 _bladePos)
    {
        if (gameObject.activeSelf)
        {
            if (Vector2.Distance(transform.position, _bladePos) < _slashDistance)
            {
                BlockSlashed();

                isSlashed = true;
            }
        }
    }
    private void OnEnable()
    {
        MoveBlock();

        isSlashed = false;
    }

    public void BlockSlashed()
    {
        _block.SetActive(false);

        _blockShadow.SetActive(false);

        _halves.SetActive(true);

        isSlashed = true;

        GameEvents.bombSlashing.Invoke();
    }
    public void MoveBlock()
    {
        isSlashed = false;

        _block.SetActive(true);

        _blockShadow.SetActive(true);

        _halves.SetActive(false);

        _rotateAndScale.StartRotateAndSale();

        _flyingSimulation.MoveDirection();
    }

    public void BombBlow(Vector3 bombPos)
    {
        if (gameObject.activeSelf)
        {
            _flyingSimulation.BombBlow(bombPos);
        }
    }
    public void IceBlockSlashed()
    {
        _flyingSimulation.ActivateIceSpeed();
    }
}
