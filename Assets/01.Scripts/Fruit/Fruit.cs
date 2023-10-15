using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fruit : PoolableMono
{
    [HideInInspector] public FruitType Type;
    
    [Header("Force")]
    [SerializeField] private Vector2 _addForceMin;
    [SerializeField] private Vector2 _addForceMax;
    
    [Header("Spawn X Position")]
    [SerializeField] private float _spawnMinX;
    [SerializeField] private float _spawnMaxX;

    [Header("Visual")] 
    [SerializeField] private List<Sprite> _sprites;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _addForce;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Init()
    {
        transform.position = new Vector3(Random.Range(_spawnMinX, _spawnMaxX), -7.5f, 0);

        Type = (FruitType)Random.Range(0, Enum.GetValues(typeof(FruitType)).Length);
        _spriteRenderer.sprite = _sprites[(int)Type];
        
        _addForce.x = Random.Range(_addForceMin.x, _addForceMax.x);
        _addForce.y = Random.Range(_addForceMin.y, _addForceMax.y);
        print(transform.localPosition);
        if (transform.position.x > 0)
        {
            _addForce.x *= -1;
            print(1);
        }
        _rigidbody.AddForce(_addForce);
    }
}
