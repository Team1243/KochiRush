using UnityEngine;

public class PlayerStickController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private StickMovement _stickMovement;
    private Stick _stick;

    // swipe
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private bool isReady = true;
    public bool IsReady => isReady;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _stickMovement = transform.GetChild(0).GetComponent<StickMovement>();
        _stick = transform.GetChild(0).GetComponent<Stick>();
    }

    private void Start()
    {
        _playerInput.startTouchEvent += StartTouchHandle;
        _playerInput.endTouchEvent += EndTouchHandle;

        _stickMovement.moveStartEvent += () => isReady = false;
        _stick.showStickDoneEvent += () => isReady = true;

        isReady = true;
    }

    private void OnDestroy()
    {
        _playerInput.startTouchEvent -= StartTouchHandle;
        _playerInput.endTouchEvent -= EndTouchHandle;
    }

    private void Update()
    {
        Debug.DrawLine(startTouchPos, endTouchPos, Color.red);
    }

    private void StartTouchHandle(Vector2 value)
    {
        if (!isReady)
        {
            Debug.Log("is not ready");
            return;
        }

        startTouchPos = value;
    }

    private void EndTouchHandle(Vector2 value)
    {
        if (!isReady)
        {
            Debug.Log("is not ready");
            return;
        }

        endTouchPos = value;
        TryStickShoot();
    }

    private void TryStickShoot()
    {
        Vector2 dir = endTouchPos - startTouchPos;
        _stickMovement.MoveToTargetPosAndRotation(dir.normalized);
    }
}
