using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void StartScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

}
