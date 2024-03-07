using EventDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : BaseBox
{
    public static Shop instance;
    public static Shop Setup(bool smallReward = false, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<Shop>(PathPrefabs.SHOP));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }
    public TMP_Text money;

    public Button exitBtn;

    public Button buy3000Btn;
    public Button buy8000Btn;
    public Button buy15000Btn;
    public Button buy35000Btn;

    public Button resetFireRate;
    public Button resetRange;
    public Button resetDamage;
    public Button resetYear;

    public Button add1000Btn;
    public Button resetMoneyBtn;
    public Button remove1000Btn;

    public Button resetSpecialGunsBtn;

    private void Init()
    {
        exitBtn.onClick.AddListener(Close);

        buy3000Btn.onClick.AddListener(delegate { PurchaseMoney(3000); });
        buy8000Btn.onClick.AddListener(delegate { PurchaseMoney(8000); });
        buy15000Btn.onClick.AddListener(delegate { PurchaseMoney(15000); });
        buy35000Btn.onClick.AddListener(delegate { PurchaseMoney(35000); });

        resetFireRate.onClick.AddListener(delegate { ResetStat(0); });
        resetRange.onClick.AddListener(delegate { ResetStat(1); });
        resetDamage.onClick.AddListener(delegate { ResetStat(2); });
        resetYear.onClick.AddListener(delegate { ResetStat(3); });

        add1000Btn.onClick.AddListener(delegate { ChangeMoney(1000); });
        resetMoneyBtn.onClick.AddListener(delegate { ChangeMoney(0); });
        remove1000Btn.onClick.AddListener(delegate { ChangeMoney(-1000); });

        resetSpecialGunsBtn.onClick.AddListener(delegate { ResetGuns(); });

        InitState();

        this.RegisterListener(EventID.CHANGE_MONEY, MoneyUpdate);
    }

    private void InitState()
    { 
        //Reset Súng nhặt được ở cuối đường
        if (UseProfile.OwnedSpecialGuns.Length <= 0)
        {
            resetSpecialGunsBtn.interactable = false;
        }
        else
        {
            resetSpecialGunsBtn.interactable= true;
        }

        //Reset Stats
        if (UseProfile.FireRateUpgradeCount <= 0)
        {
            resetFireRate.interactable = false;
        }
        else
        {
            resetFireRate.interactable = true;
        }

        if (UseProfile.RangeUpgradeCount <= 0)
        {
            resetRange.interactable = false;
        }
        else
        { 
            resetRange.interactable= true;
        }

        if (UseProfile.DamageUpgradeCount <= 0)
        {
            resetDamage.interactable = false;
        }
        else
        { 
            resetDamage.interactable= true;
        }

        if (GamePlayController.Instance.playerContain.currentYear != GamePlayController.Instance.playerContain.startingYear)
        {
            resetYear.interactable = true;
        }
        else
        {
            resetYear.interactable = false;
        }

        money.text = UseProfile.Money.ToString() + "$";
    }

    private void MoneyUpdate(object param)
    {
        money.text = UseProfile.Money.ToString() + "$";
    }

    private void PurchaseMoney(int money)
    {
        UseProfile.Money += money;
        Debug.LogError("Money added: " + money.ToString());
        GamePlayController.Instance.gameScene.InitState();
    }

    private void ChangeMoney(int money)
    {
        if (money != 0)
        {
            UseProfile.Money += money;
        }
        else
        {
            UseProfile.Money = money;
        }
    }

    private void ResetStat(int ID)
    {
        switch (ID)
        {
            case 0:
                UseProfile.FireRateUpgradeCount = 0;
                InitState();
                break;
            case 1:
                UseProfile.RangeUpgradeCount = 0;
                InitState();
                break;
            case 2:
                UseProfile.DamageUpgradeCount = 0;
                InitState();
                break;
            case 3:
                UseProfile.YearUpgradeCount = 0;
                InitState();
                break;
        }

        GamePlayController.Instance.gameScene.InitState();
    }

    private void ResetGuns()
    {
        UseProfile.OwnedSpecialGuns = "";
        UseProfile.SpecialGunLeftHand = "";
        UseProfile.SpecialGunMiddleHand = "";
        InitState();
        Inventory.instance.InitState();
        //UseProfile.EquippedGun = 0;
        //GamePlayController.Instance.playerContain.currentGun = 0;
        //GamePlayController.Instance.playerContain.handController.GunUpdate(0);
        //Debug.LogError("Guns reset");
    }
}
