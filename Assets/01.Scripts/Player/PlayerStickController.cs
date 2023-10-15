using UnityEngine;

public class PlayerStickController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private StickMovement _stickMovement;
    // private StickCollision _stickCollision;

    // swipe
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _stickMovement = transform.GetChild(0).GetComponent<StickMovement>();
        // _stickCollision = transform.GetChild(0).GetComponent<StickCollision>(); 
    }

    private void Start()
    {
        _playerInput.startTouchEvent += StartTouchHandle;
        _playerInput.endTouchEvent += EndTouchHandle;
    }

    private void OnDestroy()
    {
        // input event 구독 해제
    }

    private void Update()
    {
        Debug.DrawLine(startTouchPos, endTouchPos, Color.red);
    }

    private void StartTouchHandle(Vector2 value)
    {
        Debug.Log("touch start");
        startTouchPos = value;
    }

    private void EndTouchHandle(Vector2 value)
    {
        Debug.Log("touch end");
        endTouchPos = value;
        
        TryStickShoot();
    }

    private void TryStickShoot()
    {
        Vector2 dir = endTouchPos - startTouchPos;
        _stickMovement.MoveToTargetPosAndRotation(dir.normalized);
    }
}
