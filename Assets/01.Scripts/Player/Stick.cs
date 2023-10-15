using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private List<Fruit> fruitList;

    private Transform fruitBasketTr;

    private Vector3 startPos;

    private void Awake()
    {
        fruitBasketTr = transform.GetChild(1).GetComponent<Transform>();
    }

    public void SetFruitList(List<Fruit> tempList)
    {
        fruitList = tempList;
        StartCoroutine(ShowStick());
    }

    private IEnumerator ShowStick()
    {
        // startPos = fruitBasketTr.position;
        Vector3 pos = fruitBasketTr.position;
        for (int i = 0; i < fruitList.Count; i++)
        {
            fruitList[i].transform.position = pos;

            // Â¦¼öÀÏ ¶§
            if (i % 2 == 0)
            {
                for (int j = 0; j < i; j++)
                {
                    pos += Vector3.one;
                }
            }
            else
            {
                for (int j = 0; j < i; j++)
                {
                    pos -= Vector3.one;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void RemoveFruitList()
    {
        fruitList.Clear();
    }
}
