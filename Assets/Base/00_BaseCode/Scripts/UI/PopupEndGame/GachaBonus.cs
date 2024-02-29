using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions.ColorPicker;

public class GachaBonus : MonoBehaviour
{
    public Transform arrow;
    public Transform maxLeft;
    public Transform maxRight;

    public Text moneyBonusText;
    public Text multipler;

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
        if (check && isMoving)
        {
            arrow.position += new Vector3(-350f, 0, 0) * Time.deltaTime;
            //arrow.rotation += new Vector3(0, 0, 5f);
        }

        if (!check && isMoving)
        {
            arrow.position += new Vector3(350f, 0, 0) * Time.deltaTime;
        }

        //movement = arrow.DOLocalPath(pathTween, 5f, PathType.Linear, PathMode.TopDown2D)
        //    .SetOptions(true)
        //    .SetEase(Ease.Linear)
        //    .SetLoops(-1);

        if (arrow.position.x <= maxLeft.position.x)
        {
            check = false;
        }
        else if (arrow.position.x >= maxRight.position.x)
        {
            check = true;
        }
       
        multiply = arrow.GetComponent<Arrow>().multipler;
        multipler.text = "x" + multiply.ToString();

        moneyBonusText.text = "+" + (GamePlayController.Instance.playerContain.cash * (multiply - 1)).ToString();
    }

    public void ArrowStop()
    {
        isMoving = false;
        movement.Kill();

        
        UseProfile.Money += GamePlayController.Instance.playerContain.cash * (multiply - 1);

        //if (arrow.position.x >= x3RangeLeft.position.x && arrow.position.x <= x3RangeRight.position.x)
        //{
        //    multiply = 3;
        //    UseProfile.Money += GamePlayController.Instance.playerContain.cash * (multiply - 1);
        //    Debug.LogError("Multi = 3");
        //}
        //else if (arrow.position.x >= x2RangeLeft.position.x && arrow.position.x <= x2RangeRight.position.x)
        //{
        //    multiply = 2;
        //    UseProfile.Money += GamePlayController.Instance.playerContain.cash * (multiply - 1);
        //    Debug.LogError("Multi = 2");
        //}
        //else
        //{
        //    multiply = 1;
        //    Debug.LogError("Multi = 1");
        //}
        
        switch (multiply)
        {
            case 1:
                multipler.color = Color.white;
                break;
            case 2:
                multipler.color = Color.yellow;
                break;
            case 3:
                multipler.color = Color.green;
                break;
            case 4:
                multipler.color = Color.cyan;
                break;
        }

        StartCoroutine(WaitForSceneRestart());
    }

    public IEnumerator WaitForSceneRestart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GamePlay");
        UseProfile.GameLevel++;
    }
}
