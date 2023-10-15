using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StickCollision : MonoBehaviour
{
    private StickMovement _stickMovement;

    // private List<CircleObj> 

    private void Awake()
    {
        _stickMovement = GetComponent<StickMovement>();
    }

    private void Start()
    {
        _stickMovement.moveFinishEvent += CheckCollisionObj;
    }

    private void CheckCollisionObj()
    {
        Debug.Log("move finish");

        
    }
}
