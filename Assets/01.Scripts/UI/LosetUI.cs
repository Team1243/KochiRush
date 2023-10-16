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

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _backButton = _rootElement.Q<Button>("Container");
        _backButton.RegisterCallback<ClickEvent>(e => UIManager.Instance.GameReStart());
    }
}
