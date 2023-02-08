using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace winterStage
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        public GameObject loadPanelPrefab;

        private AsyncOperation _loadingScene;

        public SCENELIST currentScene;

        private float _minLoadingTime;

        public static Action<SCENELIST> OnLoadScene;

        public void Init()
        {
            SingleInit();
        }
        public void LoadScene(SCENELIST targetScene)
        {
            currentScene = targetScene;

            StartCoroutine(SceneLoadCoroutine(targetScene));
        }

        private IEnumerator SceneLoadCoroutine(SCENELIST targetScene)
        {
            _minLoadingTime = 0.8f;

            Instantiate(loadPanelPrefab);

            _loadingScene = SceneManager.LoadSceneAsync(targetScene.ToString());

            _loadingScene.allowSceneActivation = false;

            while (_minLoadingTime > 0)
            {
                yield return new WaitForFixedUpdate();

                _minLoadingTime -= Time.fixedDeltaTime;
            }

            _loadingScene.allowSceneActivation = true;

            OnLoadScene?.Invoke(currentScene);
        }
    }
    public enum SCENELIST
    {
        Main,
        Menu,
    }
}
