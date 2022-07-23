using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private List<IBlock> _iBlocks = new List<IBlock>();

    private Vector3 _currentPosition;

    void Start()
    {
        GameEvents.bombSlashing.AddListener(BombBlow);

        GameEvents.iceBlockSlashed.AddListener(IceSlash);
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

    public void IceSlash()
    {
        foreach (IBlock iblock in _iBlocks)
        {
            iblock.IceBlockSlashed();
        }
    }
    public void BombBlow()
    {
        foreach (IBlock iblock in _iBlocks)
        {
            iblock.BombBlow(_currentPosition);
        }
    }
}
