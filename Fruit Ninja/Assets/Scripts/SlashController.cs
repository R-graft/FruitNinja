using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    private List<IBlock> _iBlocks = new List<IBlock>();

    private Vector3 _currentPosition;

    private Vector3 _magnetPos;

    private float iceTime;

    private float magnetTime;

    void Start()
    {
        GameEvents.bombSlashing.AddListener(BombSlash);

        GameEvents.iceBlockSlashed.AddListener(SlashIceBlock);

        GameEvents.magnetBlockSlashed.AddListener(SlashMagnetBlock);

        GameEvents.BascetBlockSlashed.AddListener(SetBasketPosition);
    }
    public void AddIblock(IBlock iblock)
    {
        _iBlocks.Add(iblock);
    }

    public void CheckSlash(Vector2 bladePosition)
    {
        _currentPosition = bladePosition;

        foreach (IBlock iblock in _iBlocks)
        {
            iblock.CheckSlash(bladePosition);
        }
    }

    private void SlashIceBlock()
    {
        StartCoroutine(IceSpeed());
    }

    private void SlashMagnetBlock()
    {
        _magnetPos = _currentPosition;

        StartCoroutine(SetMagnetMove(_magnetPos));
    }
    private IEnumerator SetMagnetMove(Vector2 magnetPos)
    {
        magnetTime += 5;

        foreach (IBlock iblock in _iBlocks)
        {
            iblock.MoveMagnet(magnetPos, true);
        }
        while (magnetTime > 0)
        {
            yield return new WaitForFixedUpdate();

            magnetTime -= Time.fixedDeltaTime;
        }
        foreach (IBlock iblock in _iBlocks)
        {
            iblock.MoveMagnet(magnetPos, false);
        }
    }
    private IEnumerator IceSpeed()
    {
        iceTime += 5;

        foreach (IBlock iblock in _iBlocks)
        {
            iblock.SetIceSpeed("ice");
        }
        while (iceTime > 0)
        {
            yield return new WaitForFixedUpdate();

            iceTime -= Time.fixedDeltaTime;
        }
        foreach (IBlock iblock in _iBlocks)
        {
            iblock.SetIceSpeed("normal");
        }
    }
    private void BombSlash()
    {
        foreach (IBlock iblock in _iBlocks)
        {
            iblock.BombSlash(_currentPosition);
        }
    }
    private void SetBasketPosition()
    {
        SpawnManager.basketPosition = _currentPosition;
    }
}
