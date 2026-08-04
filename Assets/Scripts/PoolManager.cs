using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance {get; private set;}
    private void Awake()
    {
        Instance = this;
    }
    private Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();
    public void RegisterPrefab(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            Pool newPool = new GameObject(prefab.name + " Pool").AddComponent<Pool>();
            newPool.prefab = prefab;
            newPool.transform.parent = transform;
            pools.Add(prefab, newPool);
        }
    }
    public GameObject GetObject(GameObject prefab, Vector3 position, bool isTurnedOff = false)
    {
        if (pools.TryGetValue(prefab, out Pool pool))
        {
            return pool.InstantiateObject(position, isTurnedOff);
        }
        else
        {
            RegisterPrefab(prefab);
            return pools[prefab].InstantiateObject(position, isTurnedOff);
        }
    }
}
