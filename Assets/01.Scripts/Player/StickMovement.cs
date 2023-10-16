using System.Collections;
using UnityEngine;
using System;   

public class StickMovement : MonoBehaviour
{
    private Stick _stick;

    // movement
    [SerializeField] private float moveSpeed;
    private bool isMoving = false;

    // ray
    [SerializeField] private float maxDistance; 
    private LayerMask whatIsWall;

    private Vector2 rayDir;

    // event
    public Action moveStartEvent;
    public Action moveFinishEvent;

    // reset pos
    [SerializeField] private Transform initPos;

    private void Awake()
    {
        whatIsWall = LayerMask.GetMask("Wall");
        _stick = GetComponent<Stick>();

        _stick.showStickDoneEvent += ResetPos;
    }

    private void Update()
    {
        Debug.DrawRay(transform.localPosition, rayDir, Color.red); // for debug
    }

    #region StickMove

    public void MoveToTargetPosAndRotation(Vector2 dir)
    {
        if (isMoving) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, dir, maxDistance, whatIsWall);
        rayDir = dir; // for debug

        if (hit)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            StartCoroutine(MoveAndRotation(hit.point, rot, moveSpeed));
        }
    }

    private IEnumerator MoveAndRotation(Vector2 targetPos, Quaternion targetRot, float speed, bool isReset = false)
    {
        isMoving = true;

        if (!isReset)
        {
            moveStartEvent?.Invoke(); // 과일 발사 중지 메서드 실행
        }

        float time = 0;
        float value = 0;

        while (true)
        {
            time += Time.deltaTime;
            value = time / speed;

            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPos, value);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, value);

            if (value > 0.9f)
            {
                transform.localPosition = targetPos;
                transform.localRotation = targetRot;
                break;
            }

            yield return null;
        }

        isMoving = false;

        if (!isReset)
        {
            moveFinishEvent?.Invoke();
        }
    }

    #endregion

    #region ResetPos

    private void ResetPos()
    {
        // transform.localPosition = initPos.transform.position;
        // transform.localRotation = initPos.transform.rotation;

        if (!isMoving)
        {
            StartCoroutine(MoveAndRotation(initPos.transform.position, initPos.transform.rotation, 1f, isReset: true));
        }
    }

    #endregion
}
