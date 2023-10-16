using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Stick : MonoBehaviour
{
    private List<Fruit> fruitList;

    private Transform fruitBasketTr;

    public Action showStickDoneEvent;

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
        Fruit fruit;
        Vector3 pos = Vector3.zero;
        Vector3 dir = fruitBasketTr.up;

        if (fruitList.Count == 0)
        {
            Debug.Log("stick fruits is null");
        }
        else
        {
            for (int i = 1; i <= fruitList.Count; i++)
            {
                fruit = fruitList[i - 1];
                fruit.FruitStop();
                fruit.transform.parent = fruitBasketTr.transform;
                fruit.transform.localPosition = pos;
                fruit.transform.localRotation = Quaternion.Euler(dir);

                // 짝수일 때
                if (i % 2 == 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        pos.y += 1;
                    }
                }
                else
                {
                    for (int j = 0; j < i; j++)
                    {
                        pos.y -= 1;
                    }
                }

                yield return new WaitForSeconds(0.03f);
            }
        }

        // 점수 체크 딜레이 전에 넣어주기
        yield return new WaitForSeconds(1f);

        showStickDoneEvent?.Invoke(); // 스틱 다시 준비상태
        RemoveFruitList();
    }

    public void RemoveFruitList()
    {
        if (fruitList.Count == 0) return;
        
        fruitList.ForEach(f => f.Fade(new Color(255, 255,255, 0), 0.5f));
        fruitList.Clear();
    }
}
