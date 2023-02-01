using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace winterStage
{
    public class SpawnZoneController : MonoBehaviour
    {
        [SerializeField]
        private SpawnZone[] _spawnZones;

        private Dictionary<string, float> _percentsList;

        private Dictionary<string, (Vector2, Vector2)> _zonePoints;

        private void Awake()
        {
            Init();
        }
 
        private void Init()
        {
            _zonePoints = new Dictionary<string, (Vector2, Vector2)>();

            _percentsList = new Dictionary<string, float>();

            foreach (var zone in _spawnZones)
            {
                zone.GetPoints();

                _zonePoints.Add(zone.zoneTag, (zone.pointOne, zone.pointTwo));

                _percentsList.Add(zone.zoneTag, zone.spawnPecent);
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
    }
}
