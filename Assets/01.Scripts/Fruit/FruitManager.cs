using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitManager : MonoBehaviour
{
    [Header("Spawn Cool Time")]
    [SerializeField] private float _spawnMinCool;
    [SerializeField] private float _spawnMaxCool;

    [Header("Spawn Index")] 
    [SerializeField] private int _minSpawnIndex;
    [SerializeField] private int _maxSpawnIndex;
    private int _index;

    private void OnEnable()
    {
        StartCoroutine(SpawnCo());
    }

    private IEnumerator SpawnCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_spawnMinCool, _spawnMaxCool));

            _index = Random.Range(_minSpawnIndex, _maxSpawnIndex);

            for (int i = 0; i < _index; ++i)
            {
                PoolManager.Instance.Pop("Fruit");
            }
        }
    }

    public void FruitPush()
    {
        Fruit[] fruits;
        fruits = FindObjectsOfType<Fruit>();

        foreach (var fruit in fruits)
        {
            if (!fruit.IsCrash)
                PoolManager.Instance.Push(fruit);
        }
    }
}
