using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace winterStage
{
    public class SpawnSystem : MonoBehaviour
    {
        [Header("Configuration")]

        [Range(1, 5)] public int minBlockInPack;
        [Range(3, 12)] public int maxBlockInPack;

        [Range(1, 5)] public float packTimeScale;
        [Range(0.1f, 2)] public float spawnTimeScale;

        [SerializeField] private bool isComplicateable;

        [Header("Fields")]

        [SerializeField] private SpawnZoneController _zones;
        [SerializeField] private BlocksController _blocks;
        [SerializeField] private TransformHandler _transformer;

        private Dictionary<string, float> _percentsList;

        private Queue<Block> _currentPack = new Queue<Block>();

        private int _minBlocks;
        private int _maxBlocks;
        private float _packTime;
        private float _spawnTime;

        private const float ComplicateTimeScale = 10;

        public void Init()
        {
            SetBlocksPercents();
        }
        public void StartSystem()
        {
            _minBlocks = minBlockInPack;
            _maxBlocks = maxBlockInPack;
            _packTime= packTimeScale;
            _spawnTime= spawnTimeScale;

            StartCoroutine(SpawnBlocks());

            if (isComplicateable)
            {
                StartCoroutine(IncrementComlicate());
            }
        }
        public void StopSystem()
        {
            StopAllCoroutines();
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
                }

                yield return new WaitForSeconds(_packTime);
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

                _minBlocks++;
                _maxBlocks++;

                if (_packTime > 0)
                {
                    _packTime -= 0.3f;
                }

                if (_spawnTime > 0)
                {
                    _spawnTime -= 0.1f;
                }
            }
        }
    }
}
