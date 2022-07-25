using UnityEngine;
public interface IBlock
{
    public void CheckSlash(Vector2 bladePoint);

    public void BombSlash (Vector3 bombPosition);

    public void SetIceSpeed(string speedMode);
    public void BlockSlashed();
    public void BlockMove();
}
