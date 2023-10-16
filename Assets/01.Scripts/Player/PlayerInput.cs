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

    private PlayerStickController _stickController;

    private void Awake()
    {
        _stickController = GetComponent<PlayerStickController>();

        if (_playerInputAction == null)
        {
            _playerInputAction = new Controls();
            _playerInputAction.Player.SetCallbacks(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }

        ActivateInput();
    }

    private void Update()
    {
        if (_stickController.IsReady)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouchEvent?.Invoke(currentTouchPos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endTouchEvent?.Invoke(currentTouchPos);
            }
        }
    }

    public void ActivateInput()
    {
        _playerInputAction.Player.Enable();
    }

    public void DeActivateInput()
    {
        _playerInputAction.Player.Disable();
    }

    // 터치시 포지션을 스크린 좌표에서 월드 좌표로 변환하여 보내줌
    public void OnTouchInput(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     Debug.Log(currentTouchPos);
        //     startTouchEvent?.Invoke(currentTouchPos);
        // }
        // else if (context.canceled)
        // {
        //     Debug.Log(currentTouchPos);
        //     endTouchEvent?.Invoke(currentTouchPos);
        // }
    }

    public void OnTouchPos(InputAction.CallbackContext context)
    {
        currentTouchPos = _playerInputAction.Player.TouchPos.ReadValue<Vector2>();
        currentTouchPos.z = Camera.main.nearClipPlane;
        currentTouchPos = Camera.main.ScreenToWorldPoint(currentTouchPos);
    }
}
