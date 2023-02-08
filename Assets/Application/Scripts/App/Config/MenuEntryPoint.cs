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
            _progressController.Init();

            _scenesManager.Init();

            _menuUI.Init();
        }
    }
}