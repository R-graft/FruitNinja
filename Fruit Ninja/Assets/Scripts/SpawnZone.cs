using UnityEngine;

[CreateAssetMenu(fileName = "new Spawn Zone", menuName = "Spawn Zone")]
public class SpawnZone : ScriptableObject
{
    public string zoneName;

    public Transform pointOne;

    public Transform pointTwo;

    public SpawnZone FinishZone;

    public int heightLimitation;
}
