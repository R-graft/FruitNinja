using UnityEngine;
public interface IBlock
{
    public void CheckSlash(Vector2 bladePoint);
    public void BlockSlashed();
    public void BlockMove();

    public void BombSlash (Vector3 bombPosition);

    public void SetIceSpeed(string speedMode);

    public void MoveMagnet(Vector2 magnetPosition, bool isMove);
}
