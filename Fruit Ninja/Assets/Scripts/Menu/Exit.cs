using UnityEngine;

public class Exit : MonoBehaviour
{
    public void AppExit()
    {
        Application.Quit();

       // UnityEditor.EditorApplication.isPlaying = false;
    }
}
