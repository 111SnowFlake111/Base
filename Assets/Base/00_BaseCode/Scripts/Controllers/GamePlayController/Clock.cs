using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TMP_Text bonusGained;

    int hitCount = 0;

    bool isHitAble = true;

    private void Start()
    {
        bonusGained.text = "+" + hitCount + " Year";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet") && isHitAble)
        {
            SimplePool2.Despawn(other.gameObject);
            GamePlayController.Instance.playerContain.currentYear++;
            GamePlayController.Instance.gameScene.InitState();

            hitCount++;
            UpdateClock();

            //hitCount++;
            //if (hitCount % numberOfHitsForBonus == 0)
            //{
            //    bonusGained += bonusPerSufficientHits;
            //    GamePlayController.Instance.playerContain.bonusDamage += bonusPerSufficientHits / 100;
            //    GamePlayController.Instance.gameScene.InitState();
            //}

            //UpdateClock();
        }

        if (other.tag.Contains("Player"))
        {
            isHitAble = false;
            gameObject.transform.DOMoveY(15f, 2f).OnComplete(() => { Destroy(gameObject); });
        }
    }

    private void UpdateClock()
    {
        bonusGained.text = "+" + hitCount + " Year";
    }
}
