using System;
using System.Collections;
using UnityEngine;

namespace winterStage
{
    public class GamePlayController : MonoBehaviour
    {
        [SerializeField] private SpawnSystem _spawnSystem;
        [SerializeField] private ScreenSizeHandler _screenSizeHandler;
        [SerializeField] private SpawnZoneController _spawnZoneController;
        [SerializeField] private BlocksController _blocksController;
        [SerializeField] private GameUI _UI;
        [SerializeField] private BladeHandler _bladeHandler;
        [SerializeField] private HeartCounter _heartCounter;

        public static Action OnGameOver;

        private void Awake()
        {
            InitComponents();
        }
        private void InitComponents()
        {
            _screenSizeHandler.Init();
            _blocksController.Init();
            _spawnZoneController.Init();
            _spawnSystem.Init();
            _bladeHandler.Init();
            _UI.Init();

            RestartGame();
        }

        private void RestartGame()
        {
            _blocksController.Restart();

            _spawnSystem.StartSystem();

            _bladeHandler.EnableBlade();

            _UI.Restart();

            _heartCounter.Init();
        }

        private void SetGameOverState()
        {
            _spawnSystem.StopSystem();

            _bladeHandler.DisableBlade();

            StartCoroutine(CheckEndFruits());
        }

        private IEnumerator CheckEndFruits()
        {
            while (_blocksController.ActiveBlocks.Count > 0 || _blocksController.SlashedBlocks.Count > 0)
            {
                yield return null;
            }
            _UI.SetGameOver();

            yield break;
        }
        private void OnEnable()
        {
            OnGameOver += SetGameOverState;
        }

        private void OnDisable()
        {
            OnGameOver -= SetGameOverState;
        }
    }
}
