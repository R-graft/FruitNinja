using UnityEngine;

namespace winterStage
{
    public class ScreenSizeHandler : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;

        public const float screenHeight = 5;

        [HideInInspector] public float screenWidth;

        [HideInInspector] public float leftScreenEdge;
        [HideInInspector] public float rightScreenEdge;
        [HideInInspector] public float upScreenEdge;
        [HideInInspector] public float downScreenEdge;

        public void Init()
        {
            float aspect = m_Camera.aspect;

            screenWidth = aspect * screenHeight;

            leftScreenEdge = -screenWidth;
            rightScreenEdge = screenWidth;

            upScreenEdge = screenHeight; 
            downScreenEdge = -screenHeight;
        }

        public Vector2 GetPercentsFromPoint(Vector2 point)
        {
            return new Vector2(point.x / screenWidth, point.y / screenHeight);
        }

        public Vector2 GetPointFromPercents(Vector2 pointInPercents)
        {
            return new Vector2(pointInPercents.x * screenWidth, pointInPercents.y * screenHeight);
        }
    }
}