using UnityEngine;

namespace winterStage
{
    public class ScreenSizeHandler : MonoBehaviour
    {
        [SerializeField]private Camera m_Camera;

        [HideInInspector] public static float screenHeight;
        [HideInInspector] public static float screenWidth;

        [HideInInspector] public static float leftScreenEdge;
        [HideInInspector] public static float rightScreenEdge;
        [HideInInspector] public static float upScreenEdge;
        [HideInInspector] public static float downScreenEdge;

        public void Init()
        {
            Vector2 screenWorldSize = m_Camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            screenHeight = screenWorldSize.y;
            screenWidth = screenWorldSize.x;

            leftScreenEdge = -screenWidth;
            rightScreenEdge = screenWidth;

            upScreenEdge = screenHeight;
            downScreenEdge = -screenHeight;
        }

    }
}