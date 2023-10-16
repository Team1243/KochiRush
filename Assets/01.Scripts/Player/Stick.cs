using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Stick : MonoBehaviour
{
    private List<Fruit> fruitList;

    private Transform fruitBasketTr;

    public Action showStickDoneEvent;

    [SerializeField] private AudioClip _fruitClip;

    [SerializeField] private InGameUI _inGameUI;

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

        bool isRockExist = false;

        if (fruitList.Count == 0)
        {
            Debug.Log("stick fruits is null");
        }
        else
        {
            for (int i = 1; i <= fruitList.Count; i++)
            {
                fruit = fruitList[i - 1];

                // ���� ��ġ�� ������ ��
                if (fruit.Type == FruitType.Rock)
                {
                    isRockExist = true; 
                }

                fruit.FruitStop();
                fruit.transform.parent = fruitBasketTr.transform;
                fruit.transform.localPosition = pos;
                fruit.transform.localRotation = Quaternion.Euler(dir);

                // ���⼭ ���� ������ ���� ���
                AudioObj audioObj = PoolManager.Instance.Pop("AudioObj") as AudioObj;
                audioObj.PlayClip(_fruitClip);

                // ¦���� ��
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

                yield return new WaitForSeconds(0.1f);
            }
        }

        // ���� üũ ������ ���� �־��ֱ�
        yield return new WaitForSeconds(1f);

        showStickDoneEvent?.Invoke(); // ��ƽ �ٽ� �غ����
        RemoveFruitList();

        if (isRockExist)
        {
            Debug.Log("Game Over");
            UIManager.Instance.GameEnd();
        }
        _inGameUI.Score += fruitList.Count * 5;
    }

    public void RemoveFruitList()
    {
        if (fruitList.Count == 0) return;
        
        fruitList.ForEach(f => f.Fade(new Color(255, 255,255, 0), 0.5f));
        fruitList.Clear();
    }
}
