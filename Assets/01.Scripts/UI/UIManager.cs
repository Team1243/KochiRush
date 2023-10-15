using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private List<GameObject> _uiObjects;
    
    [HideInInspector] public bool IsShow = false;
     
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple UIManager is running");
        Instance = this;
    }

    public void UIShow(UIType type, bool value)
    {
        _uiObjects[(int)type].SetActive(value);
    }
}
