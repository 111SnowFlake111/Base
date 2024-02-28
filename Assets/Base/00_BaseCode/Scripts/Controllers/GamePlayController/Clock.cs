using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TMP_Text bonusStatus;
    public TMP_Text tips;

    public float bonusPerSufficientHits = 1;
    public int numberOfHitsForBonus = 5;

    int hitCount = 0;
    float bonusGained = 0;

    bool isHitAble = true;

    private void Start()
    {
        bonusStatus.text = hitCount.ToString() + " = Gained " + bonusGained.ToString();
        tips.text = numberOfHitsForBonus.ToString() + " -> " + bonusPerSufficientHits.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet") && isHitAble)
        {
            SimplePool2.Despawn(other.gameObject);
            GamePlayController.Instance.playerContain.currentYear++;

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
        bonusStatus.text = hitCount.ToString() + " = Gained " + bonusGained.ToString();
    }
}
