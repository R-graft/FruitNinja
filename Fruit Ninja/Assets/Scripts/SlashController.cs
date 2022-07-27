using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    private List<IBlock> _iBlocks = new List<IBlock>();

    private Vector3 _currentPosition;

    float iceTime;

    void Start()
    {
        GameEvents.bombSlashing.AddListener(BombSlash);

        GameEvents.iceBlockSlashed.AddListener(SlashIceBlock);
    }
    public void AddIblock(IBlock iblock)
    {
        _iBlocks.Add(iblock);
    }

    public void CheckSlash(Vector2 bladePosition)
    {
        foreach (IBlock iblock in _iBlocks)
        {
            iblock.CheckSlash(bladePosition);
        }
        _currentPosition = bladePosition;
    }

    public void SlashIceBlock()
    {
        StartCoroutine(SetBlockSpeed());
    }
    private IEnumerator SetBlockSpeed()
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
    public void BombSlash()
    {
        foreach (IBlock iblock in _iBlocks)
        {
            iblock.BombSlash(_currentPosition);
        }
    }
}
