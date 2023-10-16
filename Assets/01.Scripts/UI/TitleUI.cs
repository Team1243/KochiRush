using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class TitleUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootElement;
    private Button _startButton;
    private Button _soundButton;
    private Button _exitButton;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private List<Sprite> _audioSprite;
    
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (!PlayerPrefs.HasKey("Audio"))
            PlayerPrefs.SetInt("Audio", 1);
    }

    private void OnEnable()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _startButton = _rootElement.Q<Button>("Container");
        _soundButton = _rootElement.Q<Button>("SoundButton");
        _exitButton = _rootElement.Q<Button>("ExitButton");

        if (PlayerPrefs.GetInt("Audio") == 1)
            _soundButton.style.backgroundImage = new StyleBackground(_audioSprite[0]);
        else
            _soundButton.style.backgroundImage = new StyleBackground(_audioSprite[1]);
        
        _startButton.RegisterCallback<ClickEvent>(e => { e.StopPropagation(); UIManager.Instance.GameStart(); });
        _soundButton.RegisterCallback<ClickEvent>(e => { e.StopPropagation(); SoundBtnClick(); });
        _exitButton.RegisterCallback<ClickEvent>(e => { e.StopPropagation(); Application.Quit(); });
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
}
