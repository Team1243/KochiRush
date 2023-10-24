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
    private int _currentScore;
    public int CurrentScore
    {
        get => _currentScore;
    }

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

        bool isGameOver = false;

        if (fruitList.Count == 0)
        {
            isGameOver = true;     
        }
        else
        {
            for (int i = 1; i <= fruitList.Count; i++)
            {
                fruit = fruitList[i - 1];

                // 돌이 꼬치에 꽂혔을 때
                if (fruit.Type == FruitType.Rock)
                {
                    isGameOver = true; 
                }

                fruit.FruitStop();
                fruit.transform.parent = fruitBasketTr.transform;
                fruit.transform.localPosition = pos;
                fruit.transform.localRotation = Quaternion.Euler(dir);
                fruit.FruitRenderer.sortingLayerName = "Stick";

                // 여기서 과일 꽂히는 사운드 재생
                AudioObj audioObj = PoolManager.Instance.Pop("AudioObj") as AudioObj;
                audioObj.PlayClip(_fruitClip);

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

                yield return new WaitForSeconds(0.1f);
            }
        }

        // 점수 체크 딜레이 전에 넣어주기
        yield return new WaitForSeconds(1f);

        showStickDoneEvent?.Invoke(); // 스틱 다시 준비상태

        if (isGameOver)
        {
            UIManager.Instance.GameEnd();
        }
        _inGameUI.Score += fruitList.Count * 5;
        _currentScore = _inGameUI.Score;
        
        RemoveFruitList();
    }

    public void RemoveFruitList()
    {
        if (fruitList.Count == 0) return;
        
        fruitList.ForEach(f => f.Fade(new Color(255, 255,255, 0), 0.5f));
        fruitList.Clear();
    }
}
