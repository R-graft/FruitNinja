using UnityEditor;
using UnityEngine;
using TMPro;

namespace winterStage
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private ButtonElement _start;
        [SerializeField] private ButtonElement _exit;
        [SerializeField] private TextMeshProUGUI _bestScore;

        private int _bestScoreValue;

        public void Init()
        {
            _bestScoreValue = ProgressController.Instance.BestScore;

            _bestScore.text = _bestScoreValue.ToString();

            _start.SetDownAction(() => StartGame(), true);

            _exit.SetDownAction( () => ExitApplication(), true);
        }
        private void StartGame()
        {
            ScenesManager.Instance.LoadScene(SCENELIST.Main);
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
