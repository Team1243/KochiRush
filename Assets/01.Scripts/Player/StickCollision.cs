using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class StickCollision : MonoBehaviour
{
    private StickMovement _stickMovement;
    private Stick _stick;

    // ray
    private LayerMask whatIsFruit;
    private Transform fruitBasketTr;
    private BoxCollider2D fruitBasketCol;

    private List<Fruit> fruitListTemp = new List<Fruit>(5);

    public bool isChecking = false;

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
        _stickMovement.moveStartEvent += ActivateRay;
        _stickMovement.moveFinishEvent += DeactivateRay;
    }

    private void ActivateRay()
    {
        isChecking = true;
        StartCoroutine(CheckCollisionObj());
    }

    private  void DeactivateRay()
    {
        isChecking = false;
        _stick.SetFruitList(fruitListTemp);
    }

    private IEnumerator CheckCollisionObj()
    {
        RaycastHit2D[] hits;
        fruitListTemp.Clear();

        while (isChecking)
        {
            hits = Physics2D.BoxCastAll(transform.position, fruitBasketCol.size, 0f, fruitBasketTr.up, 0f, whatIsFruit);
            
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent(out Fruit fruit) && !fruit.IsCrash)
                {
                    if (fruitListTemp.Count < fruitListTemp.Capacity)
                    {
                        fruitListTemp.Add(fruit);
                        fruit.IsCrash = true;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
