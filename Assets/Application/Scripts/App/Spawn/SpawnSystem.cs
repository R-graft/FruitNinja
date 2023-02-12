using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace winterStage
{
    public class SpawnSystem : MonoBehaviour
    {
        [Header("Configuration")]

        [Range(1, 5)] public int minBlockInPack = 2;
        [Range(3, 12)] public int maxBlockInPack = 4;

        [Range(1, 3)] public float packTimeScale = 2;
        [Range(0.3f, 1)] public float spawnTimeScale = 0.3f;

        [SerializeField] private bool isComplicateable = false;

        [Header("Fields")]

        [SerializeField] private SpawnZoneController _zones;
        [SerializeField] private BlocksController _blocks;
        [SerializeField] private TransformHandler _transformer;

        private Dictionary<string, float> _percentsList;

        private Queue<Block> _currentPack = new Queue<Block>();

        public int _minBlocks;
        public int _maxBlocks;
        public float _packTime;
        public float _spawnTime;

#region(complicateable params)
        private const int MinBlocks = 2;
        private const int MaxBlocks = 4;
        private const float PackTime = 2;
        private const float SpawnTime = 0.5f;

        private const int MinBlocksLimit = 5;
        private const int MaxBlocksLimit = 12;
        private const float PackTimeLimit = 1;
        private const float SpawnTimeLimit = 0.2f;

        private const float ComplicateTimeScale = 15;
#endregion

        public void Init()
        {
            SetBlocksPercents();
        }
        public void StartSystem()
        {
            if (isComplicateable)
            {
                _minBlocks = MinBlocks;
                _maxBlocks = MaxBlocks;
                _packTime = PackTime;
                _spawnTime = SpawnTime;

                StartCoroutine(IncrementComlicate());
            }
            else
            {
                _minBlocks = minBlockInPack;
                _maxBlocks = maxBlockInPack;
                _packTime = packTimeScale;
                _spawnTime = spawnTimeScale;
            }

            StartCoroutine(SpawnBlocks());
        }
        public void StopSystem()
        {
            StopAllCoroutines();

            if (_currentPack.Count != 0)
            {
                for (int i = 0; i < _currentPack.Count; i++)
                {
                    _blocks.DeactivateBlock(_currentPack.Dequeue());
                }
            }
        }
        private IEnumerator SpawnBlocks()
        {
            while (true)
            {
                GetCurrentPack();

                while (_currentPack.Count > 0)
                {
                    yield return new WaitForSeconds(_spawnTime);

                    var newBock = _currentPack.Dequeue();

                    var newDirection = _transformer.GetParabolaMoveDirection(newBock.transform.position);

                    var newRotateValue = _transformer.GetRandomRotateValue();

                    var newScaleValue = _transformer.GetRandomScaleValue();

                    newBock.StateMashine.SetState(new ActiveState(newBock, newDirection, newRotateValue, newScaleValue));

                    _blocks.AddBlock(newBock, true);
                }

                yield return new WaitForSeconds(_packTime);
            }
        }

        private Vector2 GetSpawnPosition()
        {
            var currentZone = _zones.GetCurrentZone();

            var positionX = Random.Range(currentZone.pointOne.x, currentZone.pointTwo.x);

            var positionY = Random.Range(currentZone.pointOne.y, currentZone.pointTwo.y);

            return new Vector2(positionX, positionY);
        }
        private void GetCurrentPack()
        { 
            var packCount = Random.Range(_minBlocks, _maxBlocks);

            for (int i = 0; i < packCount; i++)
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

            foreach (var type in _blocks.blocksData.blocksModels)
            {
                _percentsList.Add(type.tag, type.spawnPercent);
            }
        }

        private IEnumerator IncrementComlicate()
        {
            while (true)
            {
                yield return new WaitForSeconds(ComplicateTimeScale);

                if (_minBlocks < MinBlocksLimit)
                {
                    _minBlocks++;
                }

                if (_maxBlocks < MaxBlocksLimit)
                {
                    _maxBlocks++;
                }

                if (_packTime > PackTimeLimit)
                {
                    _packTime -= 0.1f;
                }

                if (_spawnTime > SpawnTimeLimit)
                {
                    _spawnTime -= 0.02f;
                }

                else
                {
                    yield break;
                }
            }
        }
    }
}
