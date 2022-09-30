using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffectPool : MonoBehaviour
{
    public GameObject afterImagePrefab;
    public int poolSize;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    public static TrailEffectPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject afterImageGO = Instantiate(afterImagePrefab);
            afterImageGO.transform.SetParent(transform);
            AddToPool(afterImageGO);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }

        var instance = availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
