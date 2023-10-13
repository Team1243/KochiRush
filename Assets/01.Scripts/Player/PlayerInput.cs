using UnityEngine.InputSystem;
using UnityEngine;
using System;
using static Controls;

public class PlayerInput : MonoBehaviour, IPlayerActions
{
    private Controls _playerInputAction;

    public Action<Vector2> startTouchEvent;
    public Action<Vector2> endTouchEvent;

    private Vector2 pos;

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

    // ��ġ�� �������� ��ũ�� ��ǥ���� ���� ��ǥ�� ��ȯ�Ͽ� ������
    public void OnTouchInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pos = _playerInputAction.Player.TouchPos.ReadValue<Vector2>();
            pos = Camera.main.ScreenToWorldPoint(pos);
            startTouchEvent?.Invoke(pos);
        }
        else if (context.canceled)
        {
            pos = _playerInputAction.Player.TouchPos.ReadValue<Vector2>();
            pos = Camera.main.ScreenToWorldPoint(pos);
            endTouchEvent?.Invoke(pos);
        }
    }

    public void OnTouchPos(InputAction.CallbackContext context)
    {
        // do nothing
    }
}
