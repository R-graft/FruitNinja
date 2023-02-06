using UnityEngine;

namespace winterStage
{
    public class ScreenSizeHandler : MonoBehaviour
    {
        private Camera m_Camera;

        public static ScreenSizeHandler Instance;

        [HideInInspector] public float screenHeight;
        [HideInInspector] public float screenWidth;

        [HideInInspector] public float leftScreenEdge;
        [HideInInspector] public float rightScreenEdge;
        [HideInInspector] public float upScreenEdge;
        [HideInInspector] public float downScreenEdge;

        private void Awake()
        {
            Instance = new ScreenSizeHandler();
            Instance.Init();
        }
        public void Init()
        {
            m_Camera = Camera.main;

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