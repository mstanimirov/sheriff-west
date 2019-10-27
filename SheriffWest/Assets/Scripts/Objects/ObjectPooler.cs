using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [HideInInspector]
    public static ObjectPooler instance;

    [System.Serializable]
    public class Pool {

        public int size;
        public string poolTag;

        public Transform parent;
        public GameObject prefab;

        public GameObject AddObject() {

            var obj = Instantiate(prefab);

            obj.transform.SetParent(parent);
            obj.SetActive(false);

            return obj;

        }

    }

    [Header("General Settings: ")]
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    private void Start()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) {

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {

                GameObject obj = pool.AddObject();
                objectPool.Enqueue(obj);

            }

            poolDictionary.Add(pool.poolTag, objectPool);

        }

    }

    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation) {

        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        return objToSpawn;

    }

    public void ReturnToPool(string tag, GameObject objToReturn) {

        if (!poolDictionary.ContainsKey(tag))
            return;

        objToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objToReturn);

    }

}
