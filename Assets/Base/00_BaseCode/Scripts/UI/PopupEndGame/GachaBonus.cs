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

    public GameObject moneyEarnedBox;
    public GameObject moneyBonusBox;

    public Text moneyEarned;
    public Text moneyBonusText;
    public Text multipler;

    public int multiply = 3;


    private bool check = true;
    public bool isMoving = true;
    public bool canRun;
    public void Init()
    {
        canRun = true;

        moneyEarned.text = GamePlayController.Instance.playerContain.cash.ToString() + "$";
    }
    public void InitState()
    {
        
    }
    private void Update()
    {
        if(!canRun)
        {
            return;
        }
        if (check && isMoving)
        {
            arrow.position += new Vector3(-350f, 0, 0) * Time.deltaTime;
        }

        if (!check && isMoving)
        {
            arrow.position += new Vector3(350f, 0, 0) * Time.deltaTime;
        }

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

        //StartCoroutine(WaitForSceneRestart());
    }

    public void ArrowResume()
    {
        isMoving = true;

        multipler.color = Color.white;
    }

    public void ReceiveMoneyReward()
    {
        moneyBonusBox.transform.DOMove(moneyEarnedBox.transform.position, 1.5f).OnComplete(() =>
        {
            UseProfile.Money += GamePlayController.Instance.playerContain.cash * (multiply - 1);
            moneyBonusBox.SetActive(false);
            moneyEarned.text = (GamePlayController.Instance.playerContain.cash * multiply).ToString() + "$";
            StartCoroutine(PopupEndGame.instance.WaitForScreenChange());
        });
        
    }

    public IEnumerator WaitForSceneRestart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GamePlay");
        UseProfile.GameLevel++;
    }
}
