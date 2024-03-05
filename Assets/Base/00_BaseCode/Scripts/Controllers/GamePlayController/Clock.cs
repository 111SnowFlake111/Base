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

    Vector3 ogScale;

    private void Start()
    {
        bonusGained.text = "+" + hitCount + "\n" + "Year";

        ogScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet") && isHitAble)
        {
            SimplePool2.Despawn(other.gameObject);
            GamePlayController.Instance.playerContain.currentYear++;
            GamePlayController.Instance.gameScene.InitState();

            transform.DOScale(new Vector3(ogScale.x * 1.1f, ogScale.y * 1.1f, ogScale.z), 0.1f)
                .OnComplete(() =>
                {
                    transform.DOScale(ogScale, 0.1f);
                });

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
        bonusGained.text = "+" + hitCount + "\n" + "Year";
    }
}
