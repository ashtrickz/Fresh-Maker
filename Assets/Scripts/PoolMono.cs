using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{

    public T[] prefab { get; }
    public bool autoExpand { get; set; }

    public Transform container { get; }

    private List<T> pool;
    private int prefabIndex = 0;

    public PoolMono(T[] prefab, int capacity)
    {
        this.prefab = prefab;
        this.container = null;

        this.CreatePool(capacity);
    }

    private void CreatePool(int capacity)
    {
        this.pool = new List<T>();
        
        for (int i = 0; i < capacity; i++)
            this.CreateObject();
    }

    public PoolMono(T[] prefab, int capacity, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
        
        this.CreatePool(capacity);
    }

    public T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.prefab[prefabIndex], this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        if (prefabIndex == 6)
            prefabIndex = 0;
        else prefabIndex++;
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                mono.gameObject.SetActive(true);
                element = mono;
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;

        if (this.autoExpand)
            return this.CreateObject(true);
        
        throw new Exception($"There is no free elements in pool of type {typeof(T)}");

    }

    public void Disable()
    {
        foreach (var mono in pool)
        {
            mono.gameObject.GetComponent<Ingridient>().Disable();
        }
    }
    
}
