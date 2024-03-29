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

    public Button buySMGBtn;
    public Button buyRifleBtn;
    public Button buyShotgunBtn;
    public Button buySniperBtn;

    public Button add1000Btn;
    public Button resetMoneyBtn;
    public Button remove1000Btn;

    public Button resetGuns;

    private void Init()
    {
        exitBtn.onClick.AddListener(Close);

        buy3000Btn.onClick.AddListener(delegate { PurchaseMoney(3000); });
        buy8000Btn.onClick.AddListener(delegate { PurchaseMoney(8000); });
        buy15000Btn.onClick.AddListener(delegate { PurchaseMoney(15000); });
        buy35000Btn.onClick.AddListener(delegate { PurchaseMoney(35000); });

        buySMGBtn.onClick.AddListener(delegate { PurchaseGun("SMG"); });
        buyRifleBtn.onClick.AddListener(delegate { PurchaseGun("Rifle"); });
        buyShotgunBtn.onClick.AddListener(delegate { PurchaseGun("Shotgun"); });
        buySniperBtn.onClick.AddListener(delegate { PurchaseGun("Sniper"); });

        add1000Btn.onClick.AddListener(delegate { ChangeMoney(1000); });
        resetMoneyBtn.onClick.AddListener(delegate { ChangeMoney(0); });
        remove1000Btn.onClick.AddListener(delegate { ChangeMoney(-1000); });

        resetGuns.onClick.AddListener(delegate { ResetGuns(); });

        InitState();

        this.RegisterListener(EventID.INVENTORY_UPDATE, OwnedGunsStatus);
        this.RegisterListener(EventID.CHANGE_MONEY, MoneyUpdate);
    }

    private void InitState()
    {
        string inv = UseProfile.OwnedGuns;
        if (inv.Contains("SMG"))
        {
            buySMGBtn.interactable = false;
        }
        else
        {
            buySMGBtn.interactable = true;
        }

        if (inv.Contains("Rifle"))
        {
            buyRifleBtn.interactable = false;
        }
        else
        {
            buyRifleBtn.interactable = true;
        }

        if (inv.Contains("Shotgun"))
        {
            buyShotgunBtn.interactable = false;
        }
        else
        {
            buyShotgunBtn.interactable = true;
        }

        if (inv.Contains("Sniper"))
        {
            buySniperBtn.interactable = false;
        }
        else
        {
            buySniperBtn.interactable = true;
        }

        money.text = UseProfile.Money.ToString() + "$";
    }

    private void OwnedGunsStatus(object param)
    {
        string inv = UseProfile.OwnedGuns;
        if (inv.Contains("SMG"))
        {
            buySMGBtn.interactable = false;
        }
        else
        {
            buySMGBtn.interactable = true;
        }

        if (inv.Contains("Rifle"))
        {
            buyRifleBtn.interactable = false;
        }
        else
        {
            buyRifleBtn.interactable = true;
        }

        if (inv.Contains("Shotgun"))
        {
            buyShotgunBtn.interactable = false;
        }
        else
        {
            buyShotgunBtn.interactable = true;
        }

        if (inv.Contains("Sniper"))
        {
            buySniperBtn.interactable = false;
        }
        else
        {
            buySniperBtn.interactable = true;
        }
    }

    private void MoneyUpdate(object param)
    {
        money.text = UseProfile.Money.ToString() + "$";
    }

    private void PurchaseMoney(int money)
    {
        UseProfile.Money += money;
        Debug.LogError("Money added: " + money.ToString());
    }

    private void PurchaseGun(string gun)
    {
        switch (gun)
        {
            case "SMG":
                if (UseProfile.Money >= 500)
                {
                    UseProfile.Money -= 500;
                    UseProfile.OwnedGuns += gun + ",";
                    Debug.LogError("Gun purchased: " + gun);
                }
                else
                {
                    Debug.LogError("Not enough money");
                }
                break;
            case "Rifle":
                if (UseProfile.Money >= 10000)
                {
                    UseProfile.Money -= 10000;
                    UseProfile.OwnedGuns += gun + ",";
                    Debug.LogError("Gun purchased: " + gun);
                }
                else
                {
                    Debug.LogError("Not enough money");
                }
                break;
            case "Shotgun":
                if (UseProfile.Money >= 12000)
                {
                    UseProfile.Money -= 12000;
                    UseProfile.OwnedGuns += gun + ",";
                    Debug.LogError("Gun purchased: " + gun);
                }
                else
                {
                    Debug.LogError("Not enough money");
                }
                break;
            case "Sniper":
                if (UseProfile.Money >= 20000)
                {
                    UseProfile.Money -= 20000;
                    UseProfile.OwnedGuns += gun + ",";
                    Debug.LogError("Gun purchased: " + gun);
                }
                else
                {
                    Debug.LogError("Not enough money");
                }
                break;
        }
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

    private void ResetGuns()
    {
        UseProfile.OwnedGuns = "";
        UseProfile.EquippedGun = 0;
        GamePlayController.Instance.playerContain.currentGun = 0;
        GamePlayController.Instance.playerContain.handController.GunUpdate(0);
        Debug.LogError("Guns reset");
    }
}
