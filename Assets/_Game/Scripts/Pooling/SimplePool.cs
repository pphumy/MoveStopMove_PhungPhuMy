using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    static int DEFAULT_AMOUNT = 1;
    static Dictionary<GameObject, Pool> poolObjects = new Dictionary<GameObject, Pool>();
    static Dictionary<GameObject, Pool> poolParents = new Dictionary<GameObject, Pool>();

    public static void Preload(GameObject prefab, int amount, Transform parent)
    {
        if (!poolObjects.ContainsKey(prefab))
        {
            poolObjects.Add(prefab, new Pool(prefab, amount, parent));
        }
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = null;

        if (!poolObjects.ContainsKey(prefab) || poolObjects[prefab] == null)
        {
            poolObjects.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        obj = poolObjects[prefab].Spawn(position, rotation);

        return obj;
    }

    public static GameObject SpawnWithParent(GameObject prefab, Vector3 position, Quaternion rotation, Transform newParent)
    {
        GameObject obj = null;

        if (!poolObjects.ContainsKey(prefab) || poolObjects[prefab] == null)
        {
            poolObjects.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        obj = poolObjects[prefab].SpawnWithParent(position, rotation, newParent);

        return obj;
    }

    public static void SpawnOldest(GameObject prefab)
    {
        if (!poolObjects.ContainsKey(prefab) || poolObjects[prefab] == null)
        {
            return;
        }
        poolObjects[prefab].SpawnOldest();
    }

    public static void Despawn(GameObject obj)
    {
        if (poolParents.ContainsKey(obj))
        {
            poolParents[obj].Despawn(obj);
        }
        else
        {
            GameObject.Destroy(obj);
        }
    }

    public static GameObject DespawnOldest(GameObject prefab)
    {
        if (poolObjects.ContainsKey(prefab))
        {
            return poolObjects[prefab].DespawnOldest();
        }
        else
        {
            return null;
        }
    }

    public static GameObject DespawnNewest(GameObject prefab)
    {
        if (poolObjects.ContainsKey(prefab))
        {
            return poolObjects[prefab].DespawnNewest();
        }
        else
        {
            return null;
        }
    }

    public static void CollectAPool(GameObject prefab)
    {
        poolObjects[prefab].Collect();
    }

    public static void CollectAll()
    {
        foreach (var item in poolObjects)
        {
            item.Value.Collect();
        }
    }

    public static void ReleaseAll()
    {
        foreach (var item in poolObjects)
        {
            item.Value.Release();
        }
    }

    public static Vector3 GetFirstAcObjPos(GameObject prefab, Vector3 defaultPosition)
    {
        return poolObjects[prefab].GetFirstAcObjPos(defaultPosition);
    }

    public static int GetNumOfActiveObjs(GameObject prefab)
    {
        return poolObjects[prefab].GetNumOfActiveObjs();
    }

    public class Pool
    {
        Queue<GameObject> pools = new Queue<GameObject>();
        List<GameObject> activeObjs = new List<GameObject>();

        Transform parent;
        GameObject prefab;

        public Pool(GameObject prefab, int amount, Transform parent)
        {
            this.prefab = prefab;

            for (int i = 0; i < amount; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                poolParents.Add(obj, this);
                pools.Enqueue(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject obj = null;

            if (pools.Count == 0)
            {
                obj = GameObject.Instantiate(prefab, parent);
                poolParents.Add(obj, this);
            }
            else
            {
                obj = pools.Dequeue();
            }

            obj.transform.SetPositionAndRotation(position, rotation);

            new WaitForSeconds(Random.Range(1f, 5f));
            obj.gameObject.SetActive(true);

            activeObjs.Add(obj);
            return obj;
        }

        public GameObject SpawnWithParent(Vector3 position, Quaternion rotation, Transform newParent)
        {
            GameObject obj = null;

            if (pools.Count == 0)
            {
                obj = GameObject.Instantiate(prefab, newParent);
                poolParents.Add(obj, this);
            }
            else
            {
                obj = pools.Dequeue();
            }

            obj.transform.SetPositionAndRotation(position, rotation);
            new WaitForSeconds(Random.Range(1f, 5f));
            obj.gameObject.SetActive(true);

            activeObjs.Add(obj);
            return obj;
        }

        public void SpawnOldest()
        {
            if (pools.Count > 0)
            {
                GameObject obj = pools.Dequeue();
                obj.gameObject.SetActive(true);
                activeObjs.Add(obj);
            }
        }

        public void Despawn(GameObject obj)
        {
            activeObjs.Remove(obj);
            pools.Enqueue(obj);
            obj.gameObject.SetActive(false);
        }

        public GameObject DespawnOldest()
        {
            if (activeObjs.Count > 0)
            {
                GameObject obj = activeObjs[0];
                activeObjs.RemoveAt(0);
                pools.Enqueue(obj);
                obj.gameObject.SetActive(false);
                return obj;
            }
            else
            {
                return null;
            }
        }

        public GameObject DespawnNewest()
        {
            if (activeObjs.Count > 0)
            {
                GameObject obj = activeObjs[activeObjs.Count - 1];
                activeObjs.RemoveAt(activeObjs.Count - 1);
                pools.Enqueue(obj);
                obj.gameObject.SetActive(false);
                return obj;
            }
            else
            {
                return null;
            }
        }

        public void Collect()
        {
            while (activeObjs.Count > 0)
            {
                Despawn(activeObjs[0]);
            }
        }

        public void Release()
        {
            Collect();

            while (pools.Count > 0)
            {
                GameObject obj = pools.Dequeue();
                GameObject.Destroy(obj);
            }
        }

        public Vector3 GetFirstAcObjPos(Vector3 defaultPosition)
        {
            if (activeObjs.Count > 0)
            {
                return activeObjs[0].transform.position;
            }
            else
            {
                return defaultPosition;
            }
        }

        public int GetNumOfActiveObjs()
        {
            return activeObjs.Count;
        }
    }
}
