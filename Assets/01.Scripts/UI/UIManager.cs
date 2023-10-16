using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private UnityEvent _gameStart;
    [SerializeField] private UnityEvent _gameEnd;
    
    [HideInInspector] public bool IsShow = false;
     
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple UIManager is running");
        Instance = this;
    }

    public void GameStart()
    {
        _gameStart.Invoke();
    }
    
    public void GameEnd()
    {
        _gameEnd.Invoke();
    }
}
