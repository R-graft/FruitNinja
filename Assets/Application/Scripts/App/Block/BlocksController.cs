using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BlocksController : MonoBehaviour
    {
        [SerializeField] private ScreenSizeHandler screenSize;

        [SerializeField] private BonusController _bonus;

        public BlocksData blocksData;

        private CreateSystem _creator;

        public HashSet<Block> ActiveBlocks { get; private set; }
        public HashSet<Block> SlashedBlocks { get; private set; }

        public List<Block> AllBlocks { get; private set; }

        private ProgressController _progress;

        private const float SlashRadius = 1;

        private float _deadPoint;

        private const float DeadZoneOffset = 3;

        private const float CheckFallTime = 0.5f;

        [HideInInspector] public bool _stopGame;

        private bool _isHellMode;

        public void Init()
        {
            if (!ProgressController.Instance)
                Debug.Log("ProgressController not exist");

            else
                _progress = ProgressController.Instance;

            AllBlocks = new List<Block>();

            _creator = new CreateSystem();

            _creator.CreateBlocks(blocksData, this, _bonus);

            _deadPoint = screenSize.downScreenEdge - DeadZoneOffset;
        }

        public void Restart()
        {
            ActiveBlocks = new HashSet<Block>();

            SlashedBlocks = new HashSet<Block>();

            _stopGame = false;

            StartCoroutine(CheckFall());
        }

        public void AddBlock(Block block, bool isActive)
        {
            if (isActive)
            {
                ActiveBlocks.Add(block);
            }
            else
            {
                AllBlocks.Add(block);
            }
        }

        public Block GetBlock(string tag, Vector2 position)
        {
            var currentBlock = _creator.Pools[tag].Get(position);

            return currentBlock;
        }

        public void DeactivateBlock(Block block)
        {
            _creator.Pools[block.blockTag].Disable(block);

            block.StateMashine.SetState(new DisableState(block));
        }
        public void RemoveFromSet(Block block, HashSet<Block> chekingSet)
        {
            chekingSet.Remove(block);

            DeactivateBlock(block);
        }

        private void CheckSlash(BladeInfo bladePos)
        {
            foreach (var block in ActiveBlocks)
            {
                var currentDistance = (bladePos.currentPosition - block.transform.position).sqrMagnitude;

                if (currentDistance <= SlashRadius)
                {
                    SeriesCounter.OnSlashFruit?.Invoke(block.transform.position);

                    block.StateMashine.SetState(new CrushState(block, bladePos.currentDirection));

                    ActiveBlocks.Remove(block);

                    SlashedBlocks.Add(block);

                    if (_progress && !block.isBonus)
                    {
                        _progress.AddScore(50);
                    }

                    CheckSlash(bladePos);

                    break;
                }
            }
        }

        private void CheckFallBlocks(HashSet<Block> chekingSet, bool slashed)
        {
            if (chekingSet.Count > 0)
            {
                foreach (var block in chekingSet)
                {
                    if (block.transform.position.y < _deadPoint)
                    {
                        if (!_isHellMode && !block.isBonus && !slashed)
                        {
                            HeartCounter.OnLoseHeart?.Invoke();
                        }

                        RemoveFromSet(block, chekingSet);

                        CheckFallBlocks(chekingSet, slashed);

                        break;
                    }
                }
            }
        }
        private IEnumerator CheckFall()
        {
            while (true)
            {
                yield return new WaitForSeconds(CheckFallTime);

                CheckFallBlocks(ActiveBlocks, false);

                CheckFallBlocks(SlashedBlocks, true);

                if (_stopGame)
                {
                    FruitsIsEnd();
                }
            }
        }

        private void FruitsIsEnd()
        {
            if (ActiveBlocks.Count == 0 && SlashedBlocks.Count == 0)
            {
                GamePlayController.OnGameOver?.Invoke();

                StopAllCoroutines();
            }
        }

        public void SetHellMode(bool isHell)
        {
            _isHellMode = isHell;

        }
#region(poolFunctions)
        public void PoolOnGet(Block block, Vector3 newPosition)
        {
            block.transform.position = newPosition;

            block.gameObject.SetActive(true);
        }

        public void PoolOnCreate(Block block)
        {
            block.gameObject.SetActive(false);
        }

        public void PoolOnDisable(Block block)
        {
            block.gameObject.SetActive(false);
        }
#endregion

        private void OnEnable()
        {
            CutHandler.OnBladeCuting += CheckSlash;
        }

        private void OnDisable()
        {
            CutHandler.OnBladeCuting -= CheckSlash;
        }
    }
}
