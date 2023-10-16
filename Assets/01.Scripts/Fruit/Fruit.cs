using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fruit : PoolableMono
{
    [HideInInspector] public FruitType Type;
    [HideInInspector] public bool IsCrash = false;
    
    [Header("Force")]
    [SerializeField] private Vector2 _addForceMin;
    [SerializeField] private Vector2 _addForceMax;
    
    [Header("Spawn X Position")]
    [SerializeField] private float _spawnMinX;
    [SerializeField] private float _spawnMaxX;

    [Header("Visual")] 
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private List<Material> _materials;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particle;
    private ParticleSystemRenderer _particleRenderer;
    private Vector2 _addForce;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        _particleRenderer = transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
    }

    public override void Init()
    {
        IsCrash = false;
        _rigidbody.gravityScale = 1;
        _spriteRenderer.color = Color.white;
        transform.position = new Vector3(Random.Range(_spawnMinX, _spawnMaxX), -7.5f, 0);

        Type = (FruitType)Random.Range(0, Enum.GetValues(typeof(FruitType)).Length);
        if (Type == FruitType.Rock)
            Type = (FruitType)Random.Range(3, Enum.GetValues(typeof(FruitType)).Length);
        _spriteRenderer.sprite = _sprites[(int)Type];
        _particleRenderer.material = _materials[(int)Type];
        
        _addForce.x = Random.Range(_addForceMin.x, _addForceMax.x);
        _addForce.y = Random.Range(_addForceMin.y, _addForceMax.y);
        if (transform.position.x > 0)
            _addForce.x *= -1;
        _rigidbody.AddForce(_addForce);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -15f)
            PoolManager.Instance.Push(this);
    }

    public void FruitStop()
    {
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;
    }

    public void Fade(Color color, float time)
    {
        StopAllCoroutines();
        StartCoroutine(FadeAndDestroy(color, time));
    }

    public void ParticlePlay()
    {
        _particle.Play();
    }

    private IEnumerator FadeAndDestroy(Color color, float time)
    {
        float currentTime = 0;
        Color startColor = _spriteRenderer.color;
        while (currentTime < time)
        {
            yield return null;
            currentTime += Time.deltaTime;
            Mathf.Clamp(currentTime, 0, time);
            float t = currentTime / time;
            _spriteRenderer.color = Color.Lerp(startColor, color, t);
        }

        _spriteRenderer.color = color;

        PoolManager.Instance.Push(this);
        this.transform.parent = GameManager.Instance.transform;
    }
}
