using UnityEngine;
public interface IBlock
{
    public void CheckSlash(Vector2 bladePoint);

    public void BombBlow (Vector3 bombPosition);

    public void IceBlockSlashed();
    public void BlockSlashed();
    public void MoveBlock();
}
