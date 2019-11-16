using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }

    [System.Serializable]
    public struct ObjectToPool
    {
        public GameObject myObject;
        public int ammountToLoad;
        public ObjectType type;
    }
    public enum ObjectType
    {
        none,
        bullet
    }
    //public int testing;
    public ObjectToPool[] ObjectsToPool;
    private List<GameObject> pool;

    void Awake()
    {
        instance = this;

        pool = new List<GameObject>(ObjectsToPool.Length);
        for (int j = 0; j < ObjectsToPool.Length; j++)
        {
            for (int i = 0; i < ObjectsToPool[j].ammountToLoad; i++)
            {
                GameObject prefabInstance = Instantiate(ObjectsToPool[j].myObject);
                prefabInstance.transform.SetParent(transform);
                prefabInstance.SetActive(false);

                pool.Add(prefabInstance);
            }
        }

    }

    public GameObject GetObject(string searchTag)
    {
        foreach (GameObject obj in pool)
        {
           
            if (!obj.activeInHierarchy && obj.tag.ToString() == searchTag)
            {
                return obj;
                
            }
        }
            GameObject prefabInstance = Instantiate(checkObjectType(searchTag));
            prefabInstance.transform.SetParent(transform);
            pool.Add(prefabInstance);

            return prefabInstance;

    }

    private GameObject checkObjectType(string nametag)
    {
        foreach (ObjectToPool obj in ObjectsToPool)
        {
            if (nametag == obj.type.ToString())
            {
                return obj.myObject;
            }
        }
        return null;
    }
}
