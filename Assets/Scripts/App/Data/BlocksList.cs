using UnityEngine;

namespace winterStage
{
    [CreateAssetMenu(fileName = "BlocksList", menuName = "Data/newBlocksList")]
    public class BlocksList : ScriptableObject
    {
        public BlockType[] blocksTypes;
    }

    [System.Serializable]
    public class BlockType
    {
        public Block blockType;

        public int poolCount;

        public float spawnPercent;
    }
}
