using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanelWall : MonoBehaviour
{
    public GameObject bonusPanel;

    public TMP_Text wallHP;
    public Image hpBar;

    int hitLimit;

    float num;

    private void Start()
    {
        wallHP.text = bonusPanel.GetComponent<BonusPanel>().wallHP.ToString();
        hitLimit = bonusPanel.GetComponent<BonusPanel>().wallHitLimit;

        num = (1 / float.Parse(wallHP.text));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet"))
        {
            wallHP.text = (int.Parse(wallHP.text) - other.GetComponent<Bullet>().wallDamage).ToString();

            if(int.Parse(wallHP.text) <= 0)
            {
                //Debug.LogError("Wall destroyed");
                bonusPanel.GetComponent<BonusPanel>().hasWall = false;
                Destroy(gameObject);
            }
            else
            {     
                hpBar.fillAmount = num * int.Parse(wallHP.text);
            }           
        }

        if (other.tag == "Player" && hitLimit > 0)
        {
            GamePlayController.Instance.playerContain.isHurt = true;
            GamePlayController.Instance.playerContain.isMoving = false;
            GamePlayController.Instance.playerContain.currentYear -= 1f;
            GamePlayController.Instance.gameScene.InitState();
            hitLimit--;
        }
    }
}
