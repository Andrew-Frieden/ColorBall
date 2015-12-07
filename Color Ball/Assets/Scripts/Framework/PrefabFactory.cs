using UnityEngine;
using System.Collections;
using System;
using ColorBall.Framework;

public class PrefabFactory : MonoBehaviour
{
    //Public prefabs
    public GameObject prefab_Ball;

    //private caches
    private static GameObjectRecycler ballCache;

    private static GameObject _cachedObjects;
    private static GameObject CachedObjects
    {
        get
        {
            if (_cachedObjects == null)
                _cachedObjects = GameObject.Find("CachedObjects");
            return _cachedObjects;
        }
    }

    void Start ()
    {
        InstantiateBallPrefabs();
	}

    private void InstantiateBallPrefabs()
    {
        ballCache = new GameObjectRecycler(30, prefab_Ball, CachedObjects);
        ballCache.BuildCache();
    }

    public static Ball GetBall()
    {
        return ballCache.GetCachedItem().GetComponent<Ball>();
    }

    public static void CacheBall(GameObject ball)
    {
        ball.GetComponent<Ball>().TimeToLive = 5;
        ballCache.CacheItem(ball);
    }
}
