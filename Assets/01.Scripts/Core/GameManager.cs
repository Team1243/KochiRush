using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private PoolingListSO _initPoolingList;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple GameManager is Running");
        Instance = this;
        
        CreatePool();
    }

    private void CreatePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        _initPoolingList.PoolList.ForEach(p => PoolManager.Instance.CreatePool(p.Prefab, p.Count));
    }
}
