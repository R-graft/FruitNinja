using UnityEngine;

namespace winterStage
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private MenuUI _menuUI;

        [SerializeField] private ProgressController _progressController;

        [SerializeField] private ScenesManager _scenesManager;

        private void Awake()
        {
            if (ProgressController.Instance == null)
            {
                _progressController.Init();
            }
            if (ScenesManager.Instance == null)
            {
                _scenesManager.Init();
            }

            _menuUI.Init();
        }
    }
}