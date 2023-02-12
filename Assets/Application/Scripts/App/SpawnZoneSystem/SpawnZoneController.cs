using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace winterStage
{
    public class SpawnZoneController : MonoBehaviour
    {
        [SerializeField] ScreenSizeHandler ScreenSize;

        [SerializeField] private List<Zone> _spawnZones;

        private Dictionary<string, float> _percentsList;

        private Dictionary<string, Zone> _zonePoints;

        public void Init()
        {
            _zonePoints = new Dictionary<string, Zone>();

            _percentsList = new Dictionary<string, float>();

            foreach (var zone in _spawnZones)
            {
                zone.pointOne = ScreenSize.GetPointFromPercents(zone.pointOneScreenPercent);
                zone.pointTwo = ScreenSize.GetPointFromPercents(zone.pointTwoScreenPercent);

                _percentsList.Add(zone.zoneTag, zone.spawnPecent);

                _zonePoints.Add(zone.zoneTag, zone);
            }
        }

        public Zone GetCurrentZone()
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
 
        private void OnDrawGizmosSelected()
        {
            ScreenSize.Init();
           
            foreach (var zone in _spawnZones)
            {
                Gizmos.color = Color.red;
               
                zone.pointOne = new Vector2(zone.pointOneX, zone.pointOneY);
                zone.pointTwo = new Vector2(zone.pointTwoX, zone.pointTwoY);

                zone.pointOneScreenPercent = ScreenSize.GetPercentsFromPoint(zone.pointOne);
                zone.pointTwoScreenPercent = ScreenSize.GetPercentsFromPoint(zone.pointTwo);

                Gizmos.DrawSphere(zone.pointOne, 0.2f);
                Gizmos.DrawSphere(zone.pointTwo, 0.2f);

                Gizmos.color = Color.green;

                Gizmos.DrawLine(zone.pointOne, zone.pointTwo);
            }
        }
    

        [System.Serializable]
        public class Zone
        {
            public string zoneTag;

            [Range(-15, 15)] public float pointOneX;
            [Range(-15, 15)] public float pointOneY;
            [Range(-15, 15)] public float pointTwoX;
            [Range(-15, 15)] public float pointTwoY;

            [HideInInspector] public Vector2 pointOne;
            [HideInInspector] public Vector2 pointTwo;

            [HideInInspector] public Vector2 pointOneScreenPercent;
            [HideInInspector] public Vector2 pointTwoScreenPercent;

            public float spawnPecent;
        }
    }
}

