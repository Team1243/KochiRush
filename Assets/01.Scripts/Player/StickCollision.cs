using System.Collections.Generic;
using UnityEngine;

public class StickCollision : MonoBehaviour
{
    private StickMovement _stickMovement;
    private Stick _stick;

    // ray
    private LayerMask whatIsFruit;
    private Transform fruitBasketTr;
    private BoxCollider2D fruitBasketCol;

    private List<Fruit> fruitListTemp = new List<Fruit>(5);

    private void Awake()
    {
        _stickMovement = GetComponent<StickMovement>();
        _stick = GetComponent<Stick>();

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
        Debug.Log("check collision start");

        RaycastHit2D[] hits;
        hits = Physics2D.BoxCastAll(fruitBasketTr.position, fruitBasketCol.size, 0f, fruitBasketTr.up, 0f, whatIsFruit);

        fruitListTemp.Clear();
        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent(out Fruit fruit))
            {
                if (fruitListTemp.Count < fruitListTemp.Capacity)
                {
                    fruitListTemp.Add(fruit);
                }
            }
        }
        _stick.SetFruitList(fruitListTemp);

        Debug.Log("check collision end");
    }
}
