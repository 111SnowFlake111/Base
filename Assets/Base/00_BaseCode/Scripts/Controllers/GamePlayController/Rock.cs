using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rock : MonoBehaviour
{
    public TMP_Text rockHP;

    public float baseHP;
    public float hpMultiplier = 1;

    Vector3 ogScale;

    void Start()
    {
        if (baseHP <= 0)
        {
            baseHP = 2;
            rockHP.text = 2.ToString();
        }
        baseHP *= hpMultiplier;
        rockHP.text = (float.Parse(rockHP.text) * hpMultiplier).ToString();

        ogScale = transform.localScale;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GamePlayController.Instance.playerContain.isAlive = false;
            PopupEndGame.Setup().Show();
        }

        if (other.tag == "Bullet")
        {
            SimplePool2.Despawn(other.gameObject);
            baseHP -= other.GetComponent<Bullet>().damage;
            rockHP.text = Mathf.Round(baseHP).ToString();

            transform.DOScale(new Vector3(ogScale.x * 1.1f, ogScale.y * 1.1f, ogScale.z), 0.1f)
                .OnComplete(() =>
                {
                    transform.DOScale(ogScale, 0.1f);
                });

            if (baseHP <= 0 || Mathf.FloorToInt(float.Parse(rockHP.text)) <= 0)
            {
                Destroy(gameObject);
            }

            //rockHP.text = (Mathf.Round((float.Parse(rockHP.text) - other.GetComponent<Bullet>().damage) * 10) / 10 ).ToString();

            //if (float.Parse(rockHP.text) <= 0)
            //{
            //    Destroy(gameObject);
            //}
        }

        //if (other.tag.Contains("Bullet"))
        //{
        //    SimplePool2.Despawn(other.gameObject);
        //    switch (GamePlayController.Instance.playerContain.currentGun)
        //    {
        //        case 0:
        //            rockHP.text = ((Mathf.Round((float.Parse(rockHP.text) - 1 + GamePlayController.Instance.playerContain.bonusDamage) * 10)) / 10).ToString();
        //            break;
        //        case 1:
        //            rockHP.text = ((Mathf.Round((float.Parse(rockHP.text) - 1 - 1 * GamePlayController.Instance.playerContain.bonusDamage) * 10)) / 10).ToString();
        //            break;
        //        case 2:
        //            rockHP.text = ((Mathf.Round((float.Parse(rockHP.text) - 2 - 2 * GamePlayController.Instance.playerContain.bonusDamage) * 10)) / 10).ToString();
        //            break;
        //        case 3:
        //            rockHP.text = ((Mathf.Round((float.Parse(rockHP.text) - 4 - 4 * GamePlayController.Instance.playerContain.bonusDamage) * 10)) / 10).ToString();
        //            break;
        //        case 4:
        //            rockHP.text = ((Mathf.Round((float.Parse(rockHP.text) - 8 - 8 * GamePlayController.Instance.playerContain.bonusDamage) * 10)) / 10).ToString();
        //            break;
        //    }

        //    if (float.Parse(rockHP.text) <= 0)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
