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
    [SerializeField] private UnityEvent _gameRestart;
    [SerializeField] private UnityEvent _gameContinue;
    
    [HideInInspector] public bool IsShow = true;
     
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple UIManager is running");
        Instance = this;
    }

    private void Start()
    {
        RewardedAdManager.Instance.onUserEarnedRewardAction += GameContinue;
    }

    public void GameStart()
    {
        _gameStart.Invoke();
        IsShow = false;
    }
    
    public void GameEnd()
    {
        _gameEnd.Invoke();
        IsShow = true;
    }
    
    public void GameReStart()
    {
        _gameRestart.Invoke();
        IsShow = true;
    }

    public void GameContinue()
    {
        _gameContinue?.Invoke();
        IsShow = false;
    }
}
