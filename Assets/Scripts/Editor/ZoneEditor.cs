using UnityEditor;
using UnityEngine;

namespace winterStage
{
    public class ZoneEditor : EditorWindow
    {
        [SerializeField] private GameObject markerPointOne;
        [SerializeField] private GameObject markerPointTwo;

        private SpawnZone editingZone;

        public Vector2 zonePointOne;
        public Vector2 zonePointTwo;

        private float _horizontalDirectionOne = 0;
        private float _verticalDirectionOne = 0;
        private float _horizontalDirectionTwo = 0;
        private float _verticalDirectionTwo = 0;

        private const float DirectionOffset = 0.5f;

        private float _downPoint;
        private float _upPoint;
        private float _leftPoint;
        private float _rightPoint;

        private bool isInitialize;

        [MenuItem("Editor/ZoneEditor")]
        public static void Init()
        {
            ZoneEditor zoneEditor = GetWindow<ZoneEditor>("ZoneEditor");

            zoneEditor.Show();
        }

        private void GetEdges()
        {
            Vector2 worldScreen = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

            _downPoint = worldScreen.y - DirectionOffset;

            _leftPoint = worldScreen.x - DirectionOffset;

            _upPoint = -_downPoint + DirectionOffset;

            _rightPoint = -_leftPoint + DirectionOffset;

            markerPointOne = Instantiate(markerPointOne);
            markerPointTwo = Instantiate(markerPointTwo);
        }

        private void OnGUI()
        {
            if (editingZone == null)
            {
                GUILayout.Label("SpawnZone");
                editingZone = (SpawnZone)EditorGUILayout.ObjectField(editingZone, typeof(SpawnZone), true);

                GUILayout.Space(10);

                if (GUILayout.Button("Close"))
                {
                    Close();
                }
                    return;
            }

            if (markerPointOne == null)
            {
                GUILayout.Label("MarkerOne");
                markerPointOne = (GameObject)EditorGUILayout.ObjectField(markerPointOne, typeof(GameObject), true);
                return;
            }

            if (markerPointTwo == null)
            {
                GUILayout.Label("MarkerTwo");
                markerPointTwo = (GameObject)EditorGUILayout.ObjectField(markerPointTwo, typeof(GameObject), true);
                return;
            }

            if (!isInitialize)
            {
                GetEdges();
                isInitialize = true;
            }
            else
            {
                EditorGUILayout.Space(10);
                GUILayout.Label(editingZone.name, EditorStyles.boldLabel);

                EditorGUILayout.Space(20);
                GUILayout.Label("HorizontalOne", EditorStyles.boldLabel);
                _horizontalDirectionOne = GUI.HorizontalSlider(new Rect(25, 70, 100, 30), _horizontalDirectionOne, _leftPoint, _rightPoint);

                EditorGUILayout.Space(20);
                GUILayout.Label("VerticalOne", EditorStyles.boldLabel);
                _verticalDirectionOne = GUI.HorizontalSlider(new Rect(25, 110, 100, 30), _verticalDirectionOne, _downPoint, _upPoint);

                EditorGUILayout.Space(20);
                GUILayout.Label("HorizontalTwo", EditorStyles.boldLabel);
                _horizontalDirectionTwo = GUI.HorizontalSlider(new Rect(25, 150, 100, 30), _horizontalDirectionTwo, _leftPoint, _rightPoint);

                EditorGUILayout.Space(20);
                GUILayout.Label("VerticalTwo", EditorStyles.boldLabel);
                _verticalDirectionTwo = GUI.HorizontalSlider(new Rect(25, 190, 100, 30), _verticalDirectionTwo, _downPoint, _upPoint);

                editingZone.pointOne = new Vector2(_horizontalDirectionOne, _verticalDirectionOne);
                editingZone.pointTwo = new Vector2(_horizontalDirectionTwo, _verticalDirectionTwo);

                markerPointOne.transform.position = editingZone.pointOne;
                markerPointTwo.transform.position = editingZone.pointTwo;

                EditorGUILayout.Space(20);

                if (GUILayout.Button("End"))
                {
                    string savePointOne = JsonUtility.ToJson(editingZone.pointOne);
                    PlayerPrefs.SetString(editingZone.zoneTag + "one", savePointOne);

                    string savePointTwo = JsonUtility.ToJson(editingZone.pointTwo);
                    PlayerPrefs.SetString(editingZone.zoneTag + "two", savePointTwo);

                    DestroyImmediate(markerPointOne);
                    DestroyImmediate(markerPointTwo);

                    editingZone = null;

                    isInitialize = false;
                }
            }
        }
    }
}
