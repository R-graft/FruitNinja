using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _fruits;

    [SerializeField]
    private SpawnZone[] _zones;

    private int _gameScore;

    private int _fruitsCount = 0;

    private float _time;

    void Start()
    {
        StartCoroutine(StartFruitPack());
    }
    public IEnumerator StartFruitPack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);

            GetFruitCount();

            for (int i = 0; i < _fruitsCount; i++)
            {
                yield return new WaitForSeconds(0.5f);

                (Vector2, Vector2, int) fruitPositions = GetTrajectory();

                GameObject newFruit = Instantiate(_fruits[Random.Range(0, _fruits.Length)], fruitPositions.Item1, Quaternion.identity);

                StartCoroutine(FruitFlying(newFruit, fruitPositions.Item1, fruitPositions.Item2, fruitPositions.Item3, _time));
            }
        }
    }
    private void GetFruitCount()
    {
        if (_gameScore < 1000)
        {
            _fruitsCount = Random.Range(2, 4);

            _time = 3;
        }
        else if (_gameScore < 2000)
        {
            _fruitsCount = Random.Range(2, 5);

            _time = 2.8f;
        }
        else if (_gameScore < 3000)
        {
            _fruitsCount = Random.Range(3, 6);

            _time = 2.2f;
        }
        else if (_gameScore < 4000)
        {
            _fruitsCount = Random.Range(4, 8);

            _time = 2f;
        }
        else if (_gameScore > 5000)
        {
            _fruitsCount = Random.Range(5, _fruits.Length);

            _time = 1.9f;
        }
    }
    private (Vector2, Vector2, int) GetTrajectory()
    {
        SpawnZone currentZone = _zones[Random.Range(0, _zones.Length)];

        Vector2 start;

        start.x = Random.Range(currentZone.pointOne.position.x, currentZone.pointTwo.transform.position.x);

        start.y = Random.Range(currentZone.pointOne.position.y, currentZone.pointTwo.transform.position.y);

        Vector2 finish;

        finish.x = Random.Range(currentZone.FinishZone.pointOne.position.x, currentZone.FinishZone.pointOne.transform.position.x);

        finish.y = Random.Range(currentZone.FinishZone.pointOne.position.y, currentZone.FinishZone.pointOne.transform.position.y);

        return (start, finish, currentZone.heightLimitation);
    }
    IEnumerator FruitFlying(GameObject fruit, Vector2 pointStart, Vector2 pointFinish,float fluyingHeight, float fluingTime)
    { 
        float temp = 0;

        float currentTime = fluingTime;

        while (currentTime > 0)
        {
            yield return new WaitForFixedUpdate();

            temp += Time.deltaTime;

            temp = temp % fluingTime;

            fruit.transform.position = MathParabola.Parabola(pointStart, pointFinish, fluyingHeight, temp / fluingTime);

            currentTime -= Time.fixedDeltaTime;
        }

        Destroy(fruit);

        yield break;
    }
}
