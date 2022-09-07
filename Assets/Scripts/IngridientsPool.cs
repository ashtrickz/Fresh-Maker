using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngridientsPool : MonoBehaviour
{

    [SerializeField] private int poolCapacity;
    [SerializeField] private bool autoExpand = false;

    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Ingridient[] ingridientPrefab;

    [SerializeField] private BlenderManager blender;

    private PoolMono<Ingridient> pool;

    private void Start()
    {
        this.pool = new PoolMono<Ingridient>(this.ingridientPrefab, this.poolCapacity, this.transform);
        this.pool.autoExpand = this.autoExpand;
    }
    
    public void SpawnIngridients()
    {
        if(!blender.isReady)
            this.CreateIngridient();
        else 
            throw new Exception("Round has not ended! Please end current round to spawn any ingridients again!");
    }

    private void CreateIngridient()
    {
        for (int i = 0; i < poolCapacity; i++)
        {
            var ingridient = this.pool.GetFreeElement();
            ingridient.transform.position = spawnPoint[i].position;
        }
    }

    public void DisablePool()
    {
        pool.Disable();
    }
}
