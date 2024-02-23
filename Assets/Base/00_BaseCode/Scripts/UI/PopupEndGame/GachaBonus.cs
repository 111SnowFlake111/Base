using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaBonus : MonoBehaviour
{
    public Transform arrow;
    public Transform maxLeft;
    public Transform maxRight;

    public Transform x3RangeLeft;
    public Transform x3RangeRight;
    public Transform x2RangeLeft;
    public Transform x2RangeRight;

    public Vector3[] pathTween;

    public int multiply = 3;

    private Tween movement;
    private bool check = true;
    public bool isMoving = true;
    public void Init()
    {

    }
    public void InitState()
    {
        
    }
    private void Update()
    {
        //if (check && isMoving)
        //{
        //    arrow.position += new Vector3(-350f, 0, 0) * Time.deltaTime;
        //    //arrow.rotation += new Vector3(0, 0, 5f);
        //}

        //if (!check && isMoving)
        //{
        //    arrow.position += new Vector3(350f, 0, 0) * Time.deltaTime;
        //}

        movement = arrow.DOLocalPath(pathTween, 5f, PathType.Linear, PathMode.TopDown2D)
            .SetOptions(true)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        if (arrow.position.x <= maxLeft.position.x)
        {
            check = false;
        }
        else if (arrow.position.x >= maxRight.position.x)
        {
            check = true;
        }
    }

    public void ArrowStop()
    {
        isMoving = false;
        movement.Kill();

        if (arrow.position.x >= x3RangeLeft.position.x && arrow.position.x <= x3RangeRight.position.x)
        {
            multiply = 3;
            Debug.LogError("Multi = 3");
        }
        else if (arrow.position.x >= x2RangeLeft.position.x && arrow.position.x <= x2RangeRight.position.x)
        {
            multiply = 2;
            Debug.LogError("Multi = 2");
        }
        else
        {
            multiply = 1;
            Debug.LogError("Multi = 1");
        } 
    }
}
