using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Range(3,8)]
    public int _difficultLevel;

    [SerializeField]
    private SpawnZoneController _zoneSettings;

    [SerializeField]
    private ObjectPool _objectPooler;

    private List<float> _percentList;

    private int _blocksCount;

    private float _spawnTime;

    private int _maxDifficultLevel;

    public static Vector2 basketPosition;

    private void Awake()
    {
        _blocksCount = 0;

        _maxDifficultLevel = 9;

        InitializeSpawnObjectsPercents();

        GameEvents.gameOver.AddListener(StopAllCoroutines);
    }
    void Start()
    {
        _spawnTime = 1;

        StartCoroutine(GenerateBlockPack());

    }
    public IEnumerator GenerateBlockPack()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_spawnTime);

            GetFruitCount();

            for (int i = 0; i < _blocksCount; i++)
            {
                yield return new WaitForSecondsRealtime(0.2f);

                string blockName = _objectPooler.pools[GetBlockTag()].tag;

                Vector2 currentStartPoint = _zoneSettings.GetStartPoint();

                _objectPooler.GrabFromPool(blockName, currentStartPoint);
            }
        }
    }
    
    private void GetFruitCount()
    {
        float time = Time.time * Time.deltaTime;

        _difficultLevel += (int)time;

        _spawnTime = _maxDifficultLevel - _difficultLevel - time > 1 ? _maxDifficultLevel - _difficultLevel - time : 1;

        int minBlocks = (_difficultLevel - 2) ;

        int maxBlocks = (_difficultLevel + 2);

        _blocksCount = Random.Range(minBlocks, maxBlocks);
    }

    private int GetBlockTag()
    {
        float total = 0;

        foreach (var value in _percentList)
        {
            total += value;
        }

        float randomValue = Random.value * total;

        for (int i = 0; i < _percentList.Count; i++)
        {
            if (randomValue < _percentList[i])
            {
                return i;
            }
            else
            {
                randomValue -= _percentList[i];
            }
        }
        return _percentList.Count - 1;
    }
    private void InitializeSpawnObjectsPercents()
    {
        _percentList = new List<float>();

        foreach (var obj in _objectPooler.pools)
        {
            _percentList.Add(obj.spawnPercent);
        }
    }
}
