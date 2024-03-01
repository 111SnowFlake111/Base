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
    public Button shop;

    //public Button btnShowInter;
    //public Button btnShowReward;
    //public Button btnBack;

    public GameObject popupPrepage;
    public Button startGameBtn;
    //public Button rangeUpgradeBtn;
    public Text yearLevel;
    public Text fireRateLevel;

    public Button yearUpgradeBtn;
    public Button fireRateUpgradeBtn;

    public Button dualWield;
    public Button tripleWield;

    bool dualWieldPressed = false;
    bool tripleWieldPressed = false;

    public bool firstGatePassed = false;

    public Text currentGunLv;
    public Text nextGunLv;
    public Text yearValue;
    public Image progressBar;

    public Text money;

    public Text level;

    public Text yearCost;
    public Text fireRateCost;
    //public Text damageCost;

    public Text range;
    public Text damage;
    public Text fireRate;


    public void Init() //Cho các hàm chạy 1 lần
    {
        setting.gameObject.SetActive(true);
        popupPrepage.SetActive(true);        
        InitState();

        //rangeUpgradeBtn.onClick.AddListener(delegate { RangeUpgrade();  });

        yearUpgradeBtn.onClick.AddListener(delegate { YearUpgrade(); });
        fireRateUpgradeBtn.onClick.AddListener(delegate { FireRateUpgrade(); });

        //startGameBtn.onClick.AddListener(delegate { HandleStartGameBtn(); });

        dualWield.onClick.AddListener(delegate { SpecialUpgrade(2); });
        tripleWield.onClick.AddListener(delegate { SpecialUpgrade(3); });

        setting.onClick.AddListener(delegate { OpenSettingWindow(); });
        inventory.onClick.AddListener(delegate { OpenInventory(); });
        shop.onClick.AddListener(delegate { OpenShop(); });

        money.text = "" + UseProfile.Money + "$";

        if (UseProfile.GameLevel >= GamePlayController.Instance.playerContain.mapController.levels.Count)
        {
            level.text = "Level " + UseProfile.GameLevel.ToString() + " (Randomized)";
        }
        else
        {
            level.text = "Level " + UseProfile.GameLevel.ToString();
        }

        this.RegisterListener(EventID.CHANGE_MONEY, MoneyUpdate);
        this.RegisterListener(EventID.LOADLEVEL, LevelChange);
    }


    public void InitState() //Cho các hàm tái sử dụng hoặc để update manually
    {  
        yearCost.text = (100 * (UseProfile.Year - 1900 + 1)).ToString();       
        fireRateCost.text = (100 * (UseProfile.FireRateUpgradeCount + 1)).ToString();

        //Phần nút nâng cấp
        yearLevel.text = "Level " + (UseProfile.Year - 1900 + 1).ToString();
        fireRateLevel.text = "Level " + (UseProfile.FireRateUpgradeCount + 1).ToString();

        if (UseProfile.Money < int.Parse(yearCost.text))
        {
            yearUpgradeBtn.interactable = false;
        }
        else
        {
            yearUpgradeBtn.interactable = true;
        }

        if (UseProfile.Money < int.Parse(fireRateCost.text))
        {
            fireRateUpgradeBtn.interactable = false;
        }
        else
        {
            fireRateUpgradeBtn.interactable = true;
        }

        //Phần tiến độ Year
        yearValue.text = Mathf.Round(GamePlayController.Instance.playerContain.currentYear).ToString();

        if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10) < 0)
        {
            currentGunLv.text = "Lv0";
            nextGunLv.text = "Lv1";
            progressBar.fillAmount = 0;
        }
        else if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10) >= GamePlayController.Instance.playerContain.handController.rightHands.Count - 1)
        {
            currentGunLv.text = "Lv" + (GamePlayController.Instance.playerContain.handController.rightHands.Count - 1);
            nextGunLv.text = "";
            progressBar.fillAmount = 1;
        }
        else
        {
            currentGunLv.text = "Lv" + Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10);
            nextGunLv.text = "Lv" + (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10) + 1);

            if ((1 - (1900 + 10 * (GamePlayController.Instance.playerContain.handController.currentGun + 1) - GamePlayController.Instance.playerContain.currentYear) / 10) >= 1)
            {
                var temp = 1 - (1900 + 10 * (GamePlayController.Instance.playerContain.handController.currentGun + 1) - GamePlayController.Instance.playerContain.currentYear) / 10;
                while (temp >= 1)
                {
                    temp -= 1;
                }

                progressBar.fillAmount = temp;
                //progressBar.fillAmount = (1 - 0.01f - (1900 + 10 * (GamePlayController.Instance.playerContain.handController.currentGun + 1) - GamePlayController.Instance.playerContain.currentYear) / 10) - 1;
            }
            else if ((1 - (1900 + 10 * (GamePlayController.Instance.playerContain.handController.currentGun + 1) - GamePlayController.Instance.playerContain.currentYear) / 10) < 0)
            {
                var temp = 1 - (1900 + 10 * (GamePlayController.Instance.playerContain.handController.currentGun + 1) - GamePlayController.Instance.playerContain.currentYear) / 10;
                while (temp < 0)
                {
                    temp++;
                }

                progressBar.fillAmount = temp;
            }
            else
            {
                progressBar.fillAmount = 1 - 0.01f - (1900 + 10 * (GamePlayController.Instance.playerContain.handController.currentGun + 1) - GamePlayController.Instance.playerContain.currentYear) / 10;
            }          
        }

        //Debug.LogError(1 - (1900 + 10 * (GamePlayController.Instance.playerContain.handController.currentGun + 1) - GamePlayController.Instance.playerContain.currentYear) / 10);

        //Kiểm tra đã chạy qua cổng đầu
        if (firstGatePassed)
        {
            if (!GamePlayController.Instance.playerContain.doubleGun && !dualWieldPressed)
            {
                dualWield.gameObject.SetActive(true);
            }
            else if (!GamePlayController.Instance.playerContain.tripleGun && !tripleWieldPressed)
            {
                tripleWield.gameObject.SetActive(true);
            }
        }
        else
        {
            dualWield.gameObject.SetActive(false);
            tripleWield.gameObject.SetActive(false);
        }

        //damageCost.text = (100 * (GamePlayController.Instance.playerContain.damageUpgradeCount + 1)).ToString();

        range.text = "Range: " + GamePlayController.Instance.playerContain.bonusRange.ToString();
        damage.text = "Damage: " + GamePlayController.Instance.playerContain.bonusDamage.ToString();
        fireRate.text = "FireRate: " + GamePlayController.Instance.playerContain.bonusFireRate.ToString();
    }

    void OpenSettingWindow()
    {
        Time.timeScale = 0;
        SettingBox.Setup().Show();
        //Setting.Setup().Show();
        //GamePlayController.Instance.playerContain.isMoving = false;
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

        shop.interactable = false;
    }

    //void RangeUpgrade()
    //{
    //    if (UseProfile.Money - int.Parse(rangeCost.text) < 0)
    //    {
    //        Debug.LogError("Not enough money for RangeUpgrade");
    //    }
    //    else
    //    {
    //        UseProfile.Money -= int.Parse(rangeCost.text);
    //        GamePlayController.Instance.playerContain.RangeUp();
    //        //GamePlayController.Instance.playerContain.rangeUpgradeCount += 1;
    //        //GamePlayController.Instance.playerContain.bonusRange += 0.1f;
    //        InitState();
    //    }
    //}

    void YearUpgrade()
    {
        if (UseProfile.Money - int.Parse(yearCost.text) < 0)
        {
            Debug.LogError("Not enough money for DamageUpgrade");
        }
        else
        {
            UseProfile.Money -= int.Parse(yearCost.text);
            GamePlayController.Instance.playerContain.YearUp();
            //GamePlayController.Instance.playerContain.damageUpgradeCount += 1;
            //GamePlayController.Instance.playerContain.bonusDamage += 0.1f;
            InitState();
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
            InitState();
        }
    }

    void SpecialUpgrade(int ID)
    {
        switch (ID)
        {
            case 2:
                GamePlayController.Instance.playerContain.doubleGun = true;
                GamePlayController.Instance.playerContain.tripleGun = false;
                dualWieldPressed = true;
                dualWield.gameObject.SetActive(false);
                GamePlayController.Instance.playerContain.handController.GunUpdate(GamePlayController.Instance.playerContain.handController.currentGun);
                InitState();
                break;
            case 3:
                GamePlayController.Instance.playerContain.doubleGun = false;
                GamePlayController.Instance.playerContain.tripleGun = true;
                dualWieldPressed = true;
                tripleWieldPressed = true;
                tripleWield.gameObject.SetActive(false);
                GamePlayController.Instance.playerContain.handController.GunUpdate(GamePlayController.Instance.playerContain.handController.currentGun);
                InitState();
                break;
        }
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {

    }

    public void MoneyUpdate(object param)
    {
        money.text = UseProfile.Money.ToString() + "$";
    }   
    
    public void LevelChange(object param)
    {
        if (UseProfile.GameLevel >= GamePlayController.Instance.playerContain.mapController.levels.Count)
        {
            level.text = "Level " + UseProfile.GameLevel.ToString() + " (Randomized)";
        }
        else
        {
            level.text = "Level " + UseProfile.GameLevel.ToString();
        }
    }
}
