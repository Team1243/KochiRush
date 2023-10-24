using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LosetUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootElement;
    private Button _backButton;
    private VisualElement _admob;
    private Button _admobButton;
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
        _admob = _rootElement.Q<VisualElement>("Admob");
        _admobButton = _admob.Q<Button>("AdmobButton");
        _scoreLabel = _rootElement.Q<Label>("ScoreText");
        _backButton.RegisterCallback<ClickEvent>(e =>
        {
            e.StopPropagation();
            UIManager.Instance.GameReStart();
        });
        _admobButton.RegisterCallback<ClickEvent>(e =>
        {
            e.StopPropagation();
            //광고 실행
            Debug.Log("Admob Show");
        });
        _scoreLabel.text = _stick.CurrentScore.ToString();
        
        // Admob << 키고 끄기
        StartCoroutine(AdmobButtonSmallCo());
    }
    
    //작아지는거 해야댐 코루틴 
    private IEnumerator AdmobButtonSmallCo()
    {
        _currentTime = 0;
        
        
        while (_currentTime < _admobButtonEnableTime)
        {
            yield return null;
            _currentTime += Time.deltaTime;
            float time = _currentTime / _admobButtonEnableTime;
            _admobButton.style.backgroundSize = new StyleBackgroundSize(new BackgroundSize(Length.Percent(Mathf.Lerp(60, 0, time)), Length.Percent(Mathf.Lerp(60, 0, time)))); 
        }

        _admobButton.visible = false;
    }
}
