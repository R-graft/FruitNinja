using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BlocksController : MonoBehaviour
    {
        [SerializeField] private ScreenSizeHandler screenSize;

        [SerializeField] private Canvas _scoreParent;

        public BlocksData blocksData;

        private CreateSystem _creator;

        public HashSet<Block> ActiveBlocks { get; private set; }

        public HashSet<Block> SlashedBlocks { get; private set; }

        private List<Block> _allBlocks = new List<Block>();

        private ProgressController _progress;

        private const float SlashRadius = 1;

        private float _deadPoint;

        private const float DeadZoneOffset = 3;

        private const float CheckFallTime = 0.5f;

        [HideInInspector] public bool _stopGame;

        public void Init()
        {
            if (!ProgressController.Instance)
                Debug.Log("ProgressController not exist");

            else
                _progress = ProgressController.Instance;


            _creator = new CreateSystem();

            _creator.CreateBlocks(blocksData, this);

            _deadPoint = screenSize.downScreenEdge - DeadZoneOffset;
        }

        public void Restart()
        {
            ActiveBlocks = new HashSet<Block>();

            SlashedBlocks = new HashSet<Block>();

            _allBlocks = new List<Block> ();

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
                _allBlocks.Add(block);
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
        private void RemoveFromSet(Block block, HashSet<Block> chekingSet)
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
                    SeriesCounter.OnSlashFruit?.Invoke();

                    block.StateMashine.SetState(new CrushState(block, bladePos.currentDirection));

                    ActiveBlocks.Remove(block);

                    SlashedBlocks.Add(block);

                    if (_progress)
                    {
                        _progress.AddScore(50);
                    }

                    CheckSlash(bladePos);

                    break;
                }
            }
        }

        private void CheckFallBlocks(HashSet<Block> chekingSet)
        {
            if (chekingSet.Count > 0)
            {
                foreach (var block in chekingSet)
                {
                    if (block.transform.position.y < _deadPoint)
                    {
                        if (block.StateMashine.CurrentState.GetType() == typeof(ActiveState))
                        {
                            HeartCounter.OnLoseHeart?.Invoke();
                        }

                        RemoveFromSet(block, chekingSet);

                        CheckFallBlocks(chekingSet);

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

                CheckFallBlocks(ActiveBlocks);

                CheckFallBlocks(SlashedBlocks);

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
            BladeHandler.OnBladeCuting += CheckSlash;
        }

        private void OnDisable()
        {
            BladeHandler.OnBladeCuting -= CheckSlash;
        }
    }
}
