using System.Collections.Generic;
using UnityEngine;

namespace ColorBall.Framework
{
    public class GameObjectRecycler
    {
        private bool hasBuiltPrefabs;
        private int cacheSize = 0;
        private GameObject prefab;
        private Queue<GameObject> cache;
        private List<GameObject> active;
        private GameObject spawnParent;
        private bool regenerate;

        private static GameObject cacheObject;
        private static GameObject CacheObject
        {
            get
            {
                if (cacheObject == null)
                    cacheObject = GameObject.Find("CachedObjects");
                return cacheObject;
            }
        }

        public GameObjectRecycler(int maxCacheSize, GameObject prefabToUse, GameObject parent, bool regenerates = false)
        {
            cacheSize = maxCacheSize;
            prefab = prefabToUse;
            cache = new Queue<GameObject>(maxCacheSize);
            active = new List<GameObject>(maxCacheSize);
            hasBuiltPrefabs = false;
            spawnParent = parent;
            regenerate = regenerates;
        }

        public void BuildCache()
        {
            for (int i = 0; i < cacheSize; i++)
            {
                var nextObject = BuildPrefab();
                CacheItem(nextObject);
            }
            hasBuiltPrefabs = true;
        }

        private GameObject BuildPrefab()
        {
            GameObject spawnedObj = (GameObject)GameObject.Instantiate(prefab);
            spawnedObj.transform.parent = spawnParent.transform;
            spawnedObj.transform.localScale = Vector3.one;
            return spawnedObj;
        }

        public void CacheItem(GameObject go)
        {
            active.Remove(go);
            if (!cache.Contains(go))
            {
                cache.Enqueue(go);
            }

            if (go != null)
            {
                go.transform.parent = spawnParent.transform;
                go.SetActive(false);
            }
        }

        public GameObject GetCachedItem()
        {
            if (!hasBuiltPrefabs)
                Debug.Log("Recycler has not built prefabs");

            if (cache.Count > 0)
            {
                GameObject cachedObj = cache.Dequeue();
                active.Add(cachedObj);
                cachedObj.SetActive(true);

                return cachedObj;
            }
            Debug.Log("Return Null w/ cache Count: " + cache.Count);
            return null;
        }

        public override string ToString()
        {
            return "Recycler (cacheSize: " + cacheSize + "  active: " + active.Count + "  cached: " + cache.Count + ")";
        }
    }
}
