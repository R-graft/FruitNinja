using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private string[] _blockTags;

    [SerializeField]
    private ZoneSettings _zoneSettings;

    [SerializeField]
    private ObjectPool _objectPooler;

    [Range(3,8)]
    public int _difficultLevel;

    private int _blocksCount = 0;

    private float _spawnTime;

    private int _maxDifficultLevel = 9;

    void Start()
    {
        _spawnTime = _difficultLevel;

        StartCoroutine(GenerateBlockPack());
    }
    public IEnumerator GenerateBlockPack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);

            GetFruitCount();

            for (int i = 0; i < _blocksCount; i++)
            {
                yield return new WaitForSeconds(0.5f);

                string blockName = "ice";// _blockTags[Random.Range(0, _blockTags.Length)];

                Vector2 currentStartPoint = _zoneSettings.GetStartPoint();

                GameObject currentBlock = _objectPooler.GrabFromPool(blockName, currentStartPoint);
            }
        }
    }
    private void GetFruitCount()
    {
        float time = Time.time * Time.deltaTime;

        _difficultLevel += (int)time;

        _spawnTime = _maxDifficultLevel - _difficultLevel - time > 1 ? _difficultLevel - time : 1;
      

        int minBlocks = (_difficultLevel - 2) ;

        int maxBlocks = (_difficultLevel + 2);

        _blocksCount = Random.Range(minBlocks, maxBlocks);
    }
}
