using UnityEngine;

namespace winterStage
{
    public class ProgressController : Singleton<ProgressController>
    {
        private GameUI _gameUI;
        public int BestScore { get; private set; }

        public int _currentScore { get; private set; }

        private const string ProgressKey = "BestScore";

        public void Init()
        {
            SingleInit();

            LoadProgress();
        }

        private void SaveProgress()
        {
            PlayerPrefs.SetInt(ProgressKey, BestScore);
        }

        private void LoadProgress()
        {
            BestScore = PlayerPrefs.GetInt(ProgressKey, BestScore);
        }

        public void AddScore(int value)
        {
            _currentScore += value;

            _gameUI.SetNewScore(value);

            if (_currentScore > BestScore)
            {
                BestScore = _currentScore;

                SaveProgress();
            }
        }

        public void SetGameUI(GameUI ui)
        {
            _gameUI = ui;
        }
    }
}
