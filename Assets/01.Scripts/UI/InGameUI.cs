using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootElement;
    private Label _scoreLabel;
    
    private int _score;
    public int Score
    {
        get
        {
            _scoreLabel.text = _score.ToString();
            return _score;
        }
        set
        {
            _score = value;
            _scoreLabel.text = _score.ToString();
        }
    }

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _scoreLabel = _rootElement.Q<Label>("ScoreText");
    }
}
