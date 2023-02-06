using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace winterStage
{
    public class SpawnSystem : MonoBehaviour
    {
        [SerializeField] private SpawnZoneController _zones;

        [SerializeField] private BlocksController _blocks;

        private DirectionHandler _directionHandler;

        private Dictionary<string, float> _percentsList;

        private Queue<Block> _currentPack = new Queue<Block>();

        private int _packCount;

        private const int _minPackCount = 2;
        private const int _maxPackCount = 5;

        private int _countMultiplier;

        private float _packTimeScale = 3f;
        private float _spawnTimeScale = 0.3f;

        private void Start()
        {
            _directionHandler = new DirectionHandler();

            SetBlocksPercents();

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

                    var newBock = _currentPack.Dequeue();

                    var newDirection = _directionHandler.GetParabolaMoveDirection(newBock.transform.position);

                    newBock.StateMashine.SetState(new ActiveState(newBock, newDirection));
                }

                yield return new WaitForSeconds(_packTimeScale);
            }
        }

        private Vector2 GetSpawnPosition()
        {
            var currentZone = _zones.GetCurrentZone();

            var positionX = Random.Range(currentZone.one.x, currentZone.two.x);

            var positionY = Random.Range(currentZone.one.y, currentZone.two.y);

            return new Vector2(positionX, positionY);
        }
        private void GetCurrentPack()
        { 
            _packCount = Random.Range(_minPackCount, _maxPackCount) + _countMultiplier;

            for (int i = 0; i < _packCount; i++)
            {
                string currentTag = GetCurrentBlockTag();

                var newPos = GetSpawnPosition();

                var block = _blocks.GetBlock(currentTag, newPos);

                _currentPack.Enqueue(block);
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

        private void SetBlocksPercents()
        {
            _percentsList = new Dictionary<string, float>();

            foreach (var type in _blocks.blocksList.blocksTypes)
            {
                _percentsList.Add(type.tag, type.spawnPercent);
            }
        }
    }
}
