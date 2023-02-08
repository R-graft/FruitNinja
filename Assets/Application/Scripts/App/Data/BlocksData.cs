using UnityEngine;

namespace winterStage
{
    [CreateAssetMenu(fileName = "BlocksData", menuName = "Data/newBlocksData")]
    public class BlocksData : ScriptableObject
    {
        public BlockModel[] blocksModels;
    }

    [System.Serializable]
    public class BlockModel
    {
        [Header ("Controller")]
        public string tag;

        public Block blockType;

        public int poolCount;

        public float spawnPercent;

        [Header("View")]
        public Sprite sprite;

        public SlashView slashView;
    }
}

