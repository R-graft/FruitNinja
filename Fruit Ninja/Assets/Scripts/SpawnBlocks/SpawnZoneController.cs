using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneController : MonoBehaviour
{
    [SerializeField]
    private SpawnZone[] _spawnZones;

    private List<float> _zonesPercents;

    void Start()
    {
        _zonesPercents = new List<float>();

        foreach (var item in _spawnZones)
        {
           _zonesPercents.Add(item.percentValue); 
        }

        transform.position = GetStartPoint();
    }

    private int GetZoneNumber()
    {
        float total = 0;

        foreach (var value in _zonesPercents)
        {
            total += value;
        }

        float randomValue = Random.value * total;

        for (int i = 0; i < _zonesPercents.Count; i++)
        {
            if (randomValue < _zonesPercents[i])
            {
                return i;
            }
            else
            {
                randomValue -= _zonesPercents[i];
            }
        }
        return _zonesPercents.Count - 1;
    }

    public Vector2 GetStartPoint()
    {
        int numberOfZone = GetZoneNumber();

        SpawnZone currentZone = _spawnZones[numberOfZone];

        float currentPosX = Random.Range(currentZone.SpawnPointStart.transform.position.x, currentZone.SpawnPointEnd.transform.position.x);

        float currentPosY = Random.Range(currentZone.SpawnPointStart.transform.position.y, currentZone.SpawnPointEnd.transform.position.y);

        return new Vector2(currentPosX,currentPosY);
    }
}
