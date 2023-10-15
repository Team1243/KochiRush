using UnityEngine;
using UnityEngine.UIElements;

public class TitleUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootElement;
    private Button _soundButton;
    private Button _exitButton;
    
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _soundButton = _rootElement.Q<Button>("SoundButton");
        _exitButton = _rootElement.Q<Button>("ExitButton");
        
        _soundButton.RegisterCallback<ClickEvent>(e => { SoundBtnClick(); });
        _exitButton.RegisterCallback<ClickEvent>(e => Application.Quit());
    }

    private void SoundBtnClick()
    {
        
    }
}
