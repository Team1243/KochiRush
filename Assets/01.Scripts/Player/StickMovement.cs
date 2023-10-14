using System.Collections;
using UnityEngine;

public class StickMovement : MonoBehaviour
{
    // movement
    [SerializeField] private float moveSpeed;
    private bool isMoving = false;

    // ray
    [SerializeField] private float maxDistance; 
    private LayerMask whatIsWall;

    private Vector2 rayDir;

    private void Awake()
    {
        whatIsWall = LayerMask.GetMask("Wall");
    }

    private void Update()
    {
        Debug.DrawRay(transform.localPosition, rayDir, Color.red);
    }

    public void MoveToTargetPosAndRotation(Vector2 dir)
    {
        if (isMoving) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, dir, maxDistance, whatIsWall);
        rayDir = dir;

        if (hit)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            StartCoroutine(MoveAndRotation(hit.point, rot, moveSpeed));
        }
    }

    private IEnumerator MoveAndRotation(Vector2 targetPos, Quaternion targetRot, float speed)
    {
        isMoving = true;

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
    }
}
