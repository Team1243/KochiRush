using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StickCollision : MonoBehaviour
{
    private StickMovement _stickMovement;

    private LayerMask whatIsFruit;

    private Transform fruitBasketTr;
    private BoxCollider2D fruitBasketCol;

    private void Awake()
    {
        _stickMovement = GetComponent<StickMovement>();
        fruitBasketTr = transform.GetChild(1).GetComponent<Transform>();
        fruitBasketCol = fruitBasketTr.GetComponent<BoxCollider2D>();

        whatIsFruit = LayerMask.GetMask("Fruit");
    }

    private void Start()
    {
        _stickMovement.moveFinishEvent += CheckCollisionObj;
    }

    private void CheckCollisionObj()
    {
        Debug.Log("move finish");
        Debug.Log("check collision start");

        RaycastHit2D[] hits;
        hits = Physics2D.BoxCastAll(fruitBasketTr.position, fruitBasketCol.size, 0f, fruitBasketTr.up, 0f, whatIsFruit);

        foreach (var hit in hits)
        {
            Debug.Log(hit.collider.gameObject.name);
        }

        Debug.Log("check collision end");
    }
}
