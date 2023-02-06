using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace winterStage
{
    public class SpawnZoneController : MonoBehaviour
    {
        [SerializeField]
        private List<Zone> _spawnZones;

        private Dictionary<string, float> _percentsList;

        private Dictionary<string, (Vector2, Vector2)> _zonePoints;

        private ScreenSizeHandler _screenSize;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _zonePoints = new Dictionary<string, (Vector2, Vector2)>();

            _percentsList = new Dictionary<string, float>();

            _screenSize = ScreenSizeHandler.Instance;

            foreach (var zone in _spawnZones)
            {
                _zonePoints.Add(zone.zoneTag, (zone.pointOne, zone.pointTwo));

                _percentsList.Add(zone.zoneTag, zone.spawnPecent);
                
                zone.pointOne = new Vector2(zone.pointOneScreenPercent.x * _screenSize.screenWidth, zone.pointOneScreenPercent.y * _screenSize.screenHeight);
                zone.pointTwo = new Vector2(zone.pointTwoScreenPercent.x * _screenSize.screenWidth, zone.pointTwoScreenPercent.y * _screenSize.screenHeight);
            }
        }

        public (Vector2 one, Vector2 two) GetCurrentZone()
        {
            float total = 0;

            foreach (var percent in _percentsList)
            {
                total += percent.Value;
            }

            float randomValue = Random.value * total;

            foreach (var percentValue in _percentsList)
            {
                if (randomValue < percentValue.Value)
                {
                    return _zonePoints[percentValue.Key];
                }

                else
                {
                    randomValue -= percentValue.Value;
                }
            }

            return _zonePoints[_percentsList.Keys.Last()];
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            _screenSize = new ScreenSizeHandler();

            _screenSize.Init();

            foreach (var zone in _spawnZones)
            {
                Gizmos.color = Color.red;

                zone.pointOneScreenPercent = new Vector2(zone.PointOneX / _screenSize.screenWidth, zone.PointOneY / _screenSize.screenHeight);
                zone.pointTwoScreenPercent = new Vector2(zone.PointTwoX / _screenSize.screenWidth, zone.PointTwoY / _screenSize.screenHeight);

                zone.pointOne = new Vector2(zone.PointOneX, zone.PointOneY);
                zone.pointTwo = new Vector2(zone.PointTwoX, zone.PointTwoY);

                Gizmos.DrawSphere(zone.pointOne, 0.2f);
                Gizmos.DrawSphere(zone.pointTwo, 0.2f);

                Gizmos.color = Color.green;

                Gizmos.DrawLine(zone.pointOne, zone.pointTwo);
            }
        }
    }
#endif

    [System.Serializable]
    public class Zone
    {
        public string zoneTag;

        [Range(-15, 15)] public float PointOneX;
        [Range(-15, 15)] public float PointOneY;
        [Range(-15, 15)] public float PointTwoX;
        [Range(-15, 15)] public float PointTwoY;

        [HideInInspector] public Vector2 pointOne;
        [HideInInspector] public Vector2 pointTwo;

         public Vector2 pointOneScreenPercent;
        public Vector2 pointTwoScreenPercent;

        public Vector2 screenPercent;

        public float spawnPecent;
    }
}

