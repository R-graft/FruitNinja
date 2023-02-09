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

        public static Action OnStopGame;

        public static Action OnGameOver;

        public static Action OnRestart;

        private void Awake()
        {
            InitComponents();
        }
        private void InitComponents()
        {
            _UI.Init();
            _screenSizeHandler.Init();
            _blocksController.Init();
            _spawnZoneController.Init();
            _spawnSystem.Init();
            _bladeHandler.Init();

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

        private void StopGame()
        {
            _spawnSystem.StopSystem();

            _bladeHandler.DisableBlade();

            _blocksController._stopGame = true;
        }

        private void GameOver()
        {
            _UI.SetGameOver();
        }

        private void OnEnable()
        {
            OnStopGame += StopGame;
            OnRestart += RestartGame;
            OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            OnStopGame -= StopGame;
            OnRestart -= RestartGame;
            OnGameOver -= GameOver;
        }
    }
}
