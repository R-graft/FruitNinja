using UnityEditor;
using UnityEngine;
using TMPro;

namespace winterStage
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private ButtonElement _start;
        [SerializeField] private ButtonElement _exit;
        [SerializeField] private ButtonElement _mode;
        [SerializeField] private TextMeshProUGUI _modeValue;
        [SerializeField] private TextMeshProUGUI _bestScore;

        private string _modeKey;

        private int _bestScoreValue;

        public void Init()
        {
            _bestScoreValue = ProgressController.Instance.BestScore;

            _bestScore.text = _bestScoreValue.ToString();

            _start.SetDownAction(() => StartGame(), true);

            _exit.SetDownAction(() => ExitApplication(), true);

            _mode.SetDownAction(() => SetBlocksMode(), true);

            LoadBlocksMode(_modeValue);
        }

        private void StartGame()
        {
            ScenesManager.Instance.LoadScene(SCENELIST.Main);
        }

        private void SetBlocksMode()
        {
            if (_modeValue.text == "2D")
            {
                _modeValue.text = "3D";

                ProgressController.Instance.mode = BlocksMode.FULL;
            }
            else
            {
                _modeValue.text = "2D";

                ProgressController.Instance.mode = BlocksMode.SIMPLE;
            }

            PlayerPrefs.SetString(_modeKey, _modeValue.text);
        }
        private void LoadBlocksMode(TextMeshProUGUI currentMode)
        {
            currentMode.text = "2D";

            PlayerPrefs.SetString(_modeKey, currentMode.text);

            ProgressController.Instance.mode = BlocksMode.SIMPLE;
        }

        public void ExitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
#if PLATFORM_ANDROID
            Application.Quit();
#endif
        }
    }
}
