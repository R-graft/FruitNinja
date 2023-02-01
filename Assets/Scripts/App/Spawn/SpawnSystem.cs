using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace winterStage
{
    public class SpawnSystem : MonoBehaviour
    {
        [SerializeField]
        private SpawnZoneController _zones;

        [SerializeField]
        private BlocksController _blocks;

        private Dictionary<string, float> _percentsList;

        private Queue<Block> _currentPack = new Queue<Block>();

        private int _packCount;

        private const int _minPackCount = 2;
        private const int _maxPackCount = 5;

        private int _countMultiplier;

        private int _spawnTimeScale = 1;

        private void Start()
        {
            SetPercents();

            StartCoroutine(SpawnBlocks());
        }
        private IEnumerator SpawnBlocks()
        {
            while (true)
            {
                GetCurrentPack();

                while (_currentPack.Count > 0)
                {
                    yield return new WaitForSeconds(_spawnTimeScale);

                    var currentBock = _currentPack.Dequeue();

                    SetSpawnPosition(currentBock);
                }
            }
        }

        private void SetSpawnPosition(Block block)
        {
            var currentZone = _zones.GetCurrentZone();

            var positionX = Random.Range(currentZone.one.x, currentZone.two.x);

            var positionY = Random.Range(currentZone.one.y, currentZone.two.y);

            block.transform.position = new Vector2(positionX, positionY);
        }
        private void GetCurrentPack()
        { 
            _packCount = Random.Range(_minPackCount, _maxPackCount) + _countMultiplier;

            for (int i = 0; i < _packCount; i++)
            {
                string currentTag = GetCurrentBlockTag();

                _currentPack.Enqueue(_blocks.GetBlock(currentTag));
            }
        }

        private string GetCurrentBlockTag()
        {
            float total = 0;

            foreach (var percent in _percentsList)
                total += percent.Value;

            float randomValue = Random.value * total;

            foreach (var percentValue in _percentsList)
            {
                if (randomValue < percentValue.Value)
                    return percentValue.Key;

                else
                    randomValue -= percentValue.Value;
            }

            return _percentsList.Keys.Last();
        }

        private void SetPercents()
        {
            _percentsList = new Dictionary<string, float>();

            foreach (var type in _blocks.blocksList.blocksTypes)
            {
                if (!_percentsList.ContainsKey(type.blockType.blockTag))
                {
                    _percentsList.Add(type.blockType.blockTag, type.spawnPercent);
                }
            }
        }
    }
}
