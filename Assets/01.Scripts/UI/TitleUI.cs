using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class TitleUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootElement;
    private VisualElement _pausePanel;
    private VisualElement _buttonContainer;
    private VisualElement _hand;
    private Button _startButton;
    private Button _pauseButton;
    private Button _soundButton;
    private Button _exitButton;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private List<Sprite> _audioSprite;

    private bool _isPause;
    
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (!PlayerPrefs.HasKey("Audio"))
            PlayerPrefs.SetInt("Audio", 1);
    }

    private void OnEnable()
    {
        _isPause = false;
        _rootElement = _uiDocument.rootVisualElement;
        _pausePanel = _rootElement.Q<VisualElement>("PausePanel");
        _buttonContainer = _rootElement.Q<VisualElement>("Buttons");
        _hand = _rootElement.Q<VisualElement>("Hand");
        _startButton = _rootElement.Q<Button>("Container");
        _pauseButton = _rootElement.Q<Button>("PauseButton");
        _soundButton = _rootElement.Q<Button>("SoundButton");
        _exitButton = _rootElement.Q<Button>("ExitButton");

        if (PlayerPrefs.GetInt("Audio") == 1)
            _soundButton.style.backgroundImage = new StyleBackground(_audioSprite[0]);
        else
            _soundButton.style.backgroundImage = new StyleBackground(_audioSprite[1]);
        
        _startButton.RegisterCallback<ClickEvent>(e => { e.StopPropagation(); UIManager.Instance.GameStart(); });
        _pauseButton.RegisterCallback<ClickEvent>(PauseButton);
        _soundButton.RegisterCallback<ClickEvent>(e => { e.StopPropagation(); SoundBtnClick(); });
        _exitButton.RegisterCallback<ClickEvent>(e => { e.StopPropagation(); Application.Quit(); });

        StartCoroutine(HandCo());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void SoundBtnClick()
    {
        if (PlayerPrefs.GetInt("Audio") == 1)
        {
            _audioMixer.SetFloat("Master", -80); //끄는거 
            _soundButton.style.backgroundImage = new StyleBackground(_audioSprite[1]);
            PlayerPrefs.SetInt("Audio", 0);
        }
        else
        {
            _audioMixer.SetFloat("Master", 0); //키는거
            _soundButton.style.backgroundImage = new StyleBackground(_audioSprite[0]);
            PlayerPrefs.SetInt("Audio", 1);
        }
    }

    private void PauseButton(ClickEvent evt)
    {
        evt.StopPropagation();
        
        if (_isPause)
        {
            _pausePanel.RemoveFromClassList("show");
            _buttonContainer.RemoveFromClassList("show");
        }
        else
        {
            _pausePanel.AddToClassList("show");
            _buttonContainer.AddToClassList("show");
        }
        _isPause = !_isPause;
    }

    private IEnumerator HandCo()
    {
        while (true)
        {
            _hand.AddToClassList("small");
            yield return new WaitForSeconds(0.5f);
            _hand.RemoveFromClassList("small");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
