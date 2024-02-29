using DG.Tweening;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    public Button setting;
    public Button startGame;

    public Button rangeUpgrade;
    public Button damageUpgrade;
    public Button fireRateUpgrade;

    public Button moneyAdd;

    public TMP_Text money;
    public TMP_Text rangeCost;
    public TMP_Text damageCost;
    public TMP_Text fireRateCost;

    public Text range;
    public Text damage;
    public Text fireRate;

    private bool clicked = false;

    public GameObject broadSetUp;


    void Start()
    {
        //rangeCost.text = (100 * (GamePlayController.Instance.playerContain.rangeUpgradeCount + 1)).ToString();
        //damageCost.text = (100 * (GamePlayController.Instance.playerContain.yearUpgradeCount + 1)).ToString();
        //fireRateCost.text = (100 * (GamePlayController.Instance.playerContain.fireRateUpgradeCount + 1)).ToString();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePlayController.Instance.playerContain.start)
        {
            setting.gameObject.SetActive(false);
            startGame.gameObject.SetActive(false);
            rangeUpgrade.gameObject.SetActive(false);
            damageUpgrade.gameObject.SetActive(false);
            fireRateUpgrade.gameObject.SetActive(false);

            moneyAdd.gameObject.SetActive(false);
        }
        else
        {
            setting.gameObject.SetActive(true);
            startGame.gameObject.SetActive(true);
            rangeUpgrade.gameObject.SetActive(true);
            damageUpgrade.gameObject.SetActive(true);
            fireRateUpgrade.gameObject.SetActive(true);

            moneyAdd.gameObject.SetActive(true);
        }

        setting.onClick.AddListener(delegate { OpenSettingWindow(); });
        startGame.onClick.AddListener(delegate { StartGame(); });

        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            rangeUpgrade.onClick.AddListener(delegate { RangeUpgrade(); });
            damageUpgrade.onClick.AddListener(delegate { DamageUpgrade(); });
            fireRateUpgrade.onClick.AddListener(delegate { FireRateUpgrade(); });
            clicked = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
        }

        moneyAdd.onClick.AddListener(delegate { MoneyAdd(); });

 
        money.text = UseProfile.Money.ToString() + " $";
 
        //range.text = "Range: " + GamePlayController.Instance.playerContain.rangeUpgradeCount.ToString();
        //damage.text = "Damage: " + GamePlayController.Instance.playerContain.yearUpgradeCount.ToString();
        //fireRate.text = "FireRate: " + GamePlayController.Instance.playerContain.fireRateUpgradeCount.ToString();
    }

    void OpenSettingWindow()
    {
        Setting.Setup().Show();
    }

    void StartGame()
    {
        GamePlayController.Instance.playerContain.isMoving = true;
        GamePlayController.Instance.playerContain.start = true;
    }

    void RangeUpgrade()
    {
        if (UseProfile.Money - int.Parse(rangeCost.text) < 0)
        {
            Debug.LogError("Not enough money for RangeUpgrade");
        }
        else
        {
            UseProfile.Money -= int.Parse(rangeCost.text);
            GamePlayController.Instance.playerContain.RangeUp();
            //GamePlayController.Instance.playerContain.rangeUpgradeCount += 1;
            //GamePlayController.Instance.playerContain.bonusRange += 0.1f;
            Start();
        }
    }

    void DamageUpgrade()
    {
        if (UseProfile.Money - int.Parse(damageCost.text) < 0)
        {
            Debug.LogError("Not enough money for DamageUpgrade");
        }
        else
        {
            UseProfile.Money -= int.Parse(damageCost.text);
            GamePlayController.Instance.playerContain.YearUp();
            //GamePlayController.Instance.playerContain.damageUpgradeCount += 1;
            //GamePlayController.Instance.playerContain.bonusDamage += 0.1f;
            Start();
        }
    }

    void FireRateUpgrade()
    {
        if (UseProfile.Money - int.Parse(fireRateCost.text) < 0)
        {
            Debug.LogError("Not enough money for FireRateUpgrade");
        }
        else
        {
            UseProfile.Money -= int.Parse(fireRateCost.text);
            GamePlayController.Instance.playerContain.FireRateUp();
            //GamePlayController.Instance.playerContain.fireRateUpgradeCount += 1;
            //GamePlayController.Instance.playerContain.bonusFireRate += 0.1f;
            Start();
        }
    }

    void MoneyAdd()
    {
        GamePlayController.Instance.playerContain.cash += 1000;
        Debug.LogError("Money added, enjoy");
    }
}
