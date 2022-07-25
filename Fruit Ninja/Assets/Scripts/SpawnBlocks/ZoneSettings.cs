using System.Collections.Generic;
using UnityEngine;

public class ZoneSettings : MonoBehaviour
{
    [SerializeField]
    private ZoneSpawn[] _spawnZones;

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
        if (total == 0)
        {
            return -1;
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

        if (numberOfZone == -1)
        {
            Debug.Log("Nothing to slash");

            return new Vector2(30, 30);
        }

        ZoneSpawn currentZone = _spawnZones[numberOfZone];

        float currentPosX = Random.Range(currentZone.SpawnPointStart.transform.position.x, currentZone.SpawnPointEnd.transform.position.x);

        float currentPosY = Random.Range(currentZone.SpawnPointStart.transform.position.y, currentZone.SpawnPointEnd.transform.position.y);

        return new Vector2(currentPosX,currentPosY);
    }
}
