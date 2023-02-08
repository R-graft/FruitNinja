using UnityEngine;

namespace winterStage
{
    public class ScreenSizeHandler : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;

        [HideInInspector] public float screenHeight;
        [HideInInspector] public float screenWidth;

        [HideInInspector] public float leftScreenEdge;
        [HideInInspector] public float rightScreenEdge;
        [HideInInspector] public float upScreenEdge;
        [HideInInspector] public float downScreenEdge;

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