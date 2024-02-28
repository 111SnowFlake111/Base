using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusPanelWall : MonoBehaviour
{
    public GameObject bonusPanel;

    public TMP_Text wallHP;

    int hitLimit;

    private void Start()
    {
        wallHP.text = bonusPanel.GetComponent<BonusPanel>().wallHP.ToString();
        hitLimit = bonusPanel.GetComponent<BonusPanel>().wallHitLimit;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet"))
        {
            wallHP.text = (int.Parse(wallHP.text) - 1).ToString();

            if(int.Parse(wallHP.text) <= 0)
            {
                Debug.LogError("Wall destroyed");
                bonusPanel.GetComponent<BonusPanel>().hasWall = false;
                Destroy(gameObject);
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
