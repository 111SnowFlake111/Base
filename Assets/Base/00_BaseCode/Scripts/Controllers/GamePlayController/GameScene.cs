using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using MoreMountains.NiceVibrations;
using UnityEngine.Events;
using TMPro;
using EventDispatcher;
using UnityEngine.SocialPlatforms.Impl;

public class GameScene : BaseScene
{
    public Button setting;
    public Button inventory;

    public Button btnShowInter;
    public Button btnShowReward;
    public Button btnBack;

    public GameObject popupPrepage;
    public Button startGameBtn;
    public Button rangeUpgradeBtn;
    public Button damageUpgradeBtn;
    public Button fireRateUpgradeBtn;

    public Button moneyAdd;

    public TMP_Text money;
    public TMP_Text rangeCost;
    public TMP_Text damageCost;
    public TMP_Text fireRateCost;

    public Text range;
    public Text damage;
    public Text fireRate;


    public void Init() //Cho các hàm chạy 1 lần
    {
        popupPrepage.SetActive(true);
        InitState();
        rangeUpgradeBtn.onClick.AddListener(delegate { RangeUpgrade();  });
        damageUpgradeBtn.onClick.AddListener(delegate { DamageUpgrade(); });
        fireRateUpgradeBtn.onClick.AddListener(delegate { FireRateUpgrade(); });

        startGameBtn.onClick.AddListener(delegate { HandleStartGameBtn(); });

        setting.onClick.AddListener(delegate { OpenSettingWindow(); });
        inventory.onClick.AddListener(delegate { OpenInventory(); });

        moneyAdd.onClick.AddListener(delegate { OpenShop(); });
        money.text = "" + UseProfile.Money + "$";

        
        this.RegisterListener(EventID.CHANGE_MONEY, MoneyUpdate);
        
    }


    public void InitState() //Cho các hàm tái sử dụng hoặc để update manually
    {
     
        rangeCost.text = (100 * (GamePlayController.Instance.playerContain.rangeUpgradeCount + 1)).ToString();
        damageCost.text = (100 * (GamePlayController.Instance.playerContain.damageUpgradeCount + 1)).ToString();
        fireRateCost.text = (100 * (GamePlayController.Instance.playerContain.fireRateUpgradeCount + 1)).ToString();

        range.text = "Range: " + GamePlayController.Instance.playerContain.bonusRange.ToString();
        damage.text = "Damage: " + GamePlayController.Instance.playerContain.bonusDamage.ToString();
        fireRate.text = "FireRate: " + GamePlayController.Instance.playerContain.bonusFireRate.ToString();
    }

    void OpenSettingWindow()
    {
        Setting.Setup().Show();
    }

    void OpenInventory()
    {
        Inventory.Setup().Show();
    }

    void OpenShop()
    {
        Shop.Setup().Show();
    }

    public void HandleStartGameBtn()
    {
        popupPrepage.gameObject.SetActive(false);
        GamePlayController.Instance.playerContain.isMoving = true;
        GamePlayController.Instance.playerContain.start = true;   
    }

    void RangeUpgrade()
    {
        if (GamePlayController.Instance.playerContain.money - int.Parse(rangeCost.text) < 0)
        {
            Debug.LogError("Not enough money for RangeUpgrade");
        }
        else
        {
            GamePlayController.Instance.playerContain.money -= int.Parse(rangeCost.text);
            GamePlayController.Instance.playerContain.RangeUp();
            //GamePlayController.Instance.playerContain.rangeUpgradeCount += 1;
            //GamePlayController.Instance.playerContain.bonusRange += 0.1f;
            InitState();
        }
    }

    void DamageUpgrade()
    {
        if (GamePlayController.Instance.playerContain.money - int.Parse(damageCost.text) < 0)
        {
            Debug.LogError("Not enough money for DamageUpgrade");
        }
        else
        {
            GamePlayController.Instance.playerContain.money -= int.Parse(damageCost.text);
            GamePlayController.Instance.playerContain.DamageUp();
            //GamePlayController.Instance.playerContain.damageUpgradeCount += 1;
            //GamePlayController.Instance.playerContain.bonusDamage += 0.1f;
            InitState();
        }
    }

    void FireRateUpgrade()
    {
        if (GamePlayController.Instance.playerContain.money - int.Parse(fireRateCost.text) < 0)
        {
            Debug.LogError("Not enough money for FireRateUpgrade");
        }
        else
        {
            GamePlayController.Instance.playerContain.money -= int.Parse(fireRateCost.text);
            GamePlayController.Instance.playerContain.FireRateUp();
            //GamePlayController.Instance.playerContain.fireRateUpgradeCount += 1;
            //GamePlayController.Instance.playerContain.bonusFireRate += 0.1f;
            InitState();
        }
    }

    void MoneyAdd()
    {
        UseProfile.Money += 1000;
        Debug.LogError("Money added, enjoy");
        InitState();
    }
    public override void OnEscapeWhenStackBoxEmpty()
    {

    }

    public void MoneyUpdate(object param)
    {
        money.text = UseProfile.Money.ToString() + "$";
    }    

}
