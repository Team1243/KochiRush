using UnityEngine.InputSystem;
using UnityEngine;
using System;
using static Controls;

public class PlayerInput : MonoBehaviour, IPlayerActions
{
    private Controls _playerInputAction;

    public Action<Vector2> startTouchEvent;
    public Action<Vector2> endTouchEvent;

    private Vector3 currentTouchPos;

    private void Awake()
    {
        if (_playerInputAction == null)
        {
            _playerInputAction = new Controls();
            _playerInputAction.Player.SetCallbacks(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
        
        _playerInputAction.Player.Enable();
    }

    // 터치시 포지션을 스크린 좌표에서 월드 좌표로 변환하여 보내줌
    public void OnTouchInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log(currentTouchPos);
            startTouchEvent?.Invoke(currentTouchPos);
        }
        else if (context.canceled)
        {
            Debug.Log(currentTouchPos);
            endTouchEvent?.Invoke(currentTouchPos);
        }
    }

    public void OnTouchPos(InputAction.CallbackContext context)
    {
        currentTouchPos = _playerInputAction.Player.TouchPos.ReadValue<Vector2>();
        currentTouchPos.z = Camera.main.nearClipPlane;
        currentTouchPos = Camera.main.ScreenToWorldPoint(currentTouchPos);
    }
}
