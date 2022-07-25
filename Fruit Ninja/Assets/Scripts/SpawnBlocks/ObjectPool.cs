using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private SlashController _collisionManager;

    [System.Serializable]
    public class Pool
    {
        public string tag;

        public GameObject prefab;

        public int poolsize;
    }

    [SerializeField]
    private Pool[] pools;

    private Dictionary<string, Queue<GameObject>> poolsDictionary;

    void Start()
    {
        poolsDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolsize; i++)
            {
                GameObject newPoolObject = Instantiate(pool.prefab);

                _collisionManager.AddIblock(newPoolObject.GetComponent<IBlock>());

                newPoolObject.SetActive(false);

                objectPool.Enqueue(newPoolObject);
            }
            poolsDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject GrabFromPool(string _tag, Vector3 _pos)
    {
        if (!poolsDictionary.ContainsKey(_tag))
        {
            Debug.Log("key not found");

            return null;
        }

        GameObject spawnObject = poolsDictionary[_tag].Dequeue();

        spawnObject.transform.position = _pos;

        spawnObject.SetActive(true);

        poolsDictionary[_tag].Enqueue(spawnObject);

        return spawnObject;
    }
}
