using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LosetUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootElement;
    private Button _backButton;
    private Label _scoreLabel;
    private Stick _stick;
    
    [SerializeField] private float _admobButtonEnableTime = 3;
    private float _currentTime = 0; 

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _stick = FindObjectOfType<Stick>();
    }

    private void OnEnable()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _backButton = _rootElement.Q<Button>("Container");
        _scoreLabel = _rootElement.Q<Label>("ScoreText");
        _backButton.RegisterCallback<ClickEvent>(e =>
        {
            e.StopPropagation();
            UIManager.Instance.GameReStart();
        });
        _scoreLabel.text = _stick.CurrentScore.ToString();
    }
}
