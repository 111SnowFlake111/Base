using EventDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : BaseBox
{
    public static Inventory instance;
    public static Inventory Setup(bool smallReward = false, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<Inventory>(PathPrefabs.GAME_INVENTORY));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public List<Button> equipButtons;
    public List<Button> goToShopButtons;
    public Button close;

    private void Init()
    {
        close.onClick.AddListener(Close);

        equipButtons[0].onClick.AddListener(delegate { EquipItem(0); });
        equipButtons[1].onClick.AddListener(delegate { EquipItem(1); });
        equipButtons[2].onClick.AddListener(delegate { EquipItem(2); });
        equipButtons[3].onClick.AddListener(delegate { EquipItem(3); });
        equipButtons[4].onClick.AddListener(delegate { EquipItem(4); });

        foreach(Button button in goToShopButtons)
        {
            button.onClick.AddListener(GoToShop);
        }

        InitState();

        this.RegisterListener(EventID.EQUIPPED_GUN, EquippedGunStatus);
        this.RegisterListener(EventID.INVENTORY_UPDATE, OwnedGunsStatus);
    }

    private void InitState()
    {
        int currentGun = UseProfile.EquippedGun;

        foreach (Button button in equipButtons)
        {
            button.interactable = true;
        }

        switch (currentGun)
        {
            case 0:
                equipButtons[0].interactable = false; break;
            case 1:
                equipButtons[1].interactable = false; break;
            case 2:
                equipButtons[2].interactable = false; break;
            case 3:
                equipButtons[3].interactable = false; break;
            case 4:
                equipButtons[4].interactable = false; break;
        }

        string inv = UseProfile.OwnedGuns;
        if (inv.Contains("SMG"))
        {
            goToShopButtons[0].gameObject.SetActive(false);
        }
        else
        {
            goToShopButtons[0].gameObject.SetActive(true);
        }

        if (inv.Contains("Rifle"))
        {
            goToShopButtons[1].gameObject.SetActive(false);
        }
        else
        {
            goToShopButtons[1].gameObject.SetActive(true);
        }

        if (inv.Contains("Shotgun"))
        {
            goToShopButtons[2].gameObject.SetActive(false);
        }
        else
        {
            goToShopButtons[2].gameObject.SetActive(true);
        }

        if (inv.Contains("Sniper"))
        {
            goToShopButtons[3].gameObject.SetActive(false);
        }
        else
        {
            goToShopButtons[3].gameObject.SetActive(true);
        }
    }

    private void EquippedGunStatus(object param)
    {
        int currentGun = UseProfile.EquippedGun;

        foreach(Button button in equipButtons)
        {
            button.interactable = true;
        }

        switch (currentGun)
        {
            case 0:
                equipButtons[0].interactable = false; break;
            case 1:
                equipButtons[1].interactable = false; break;
            case 2:
                equipButtons[2].interactable = false; break;
            case 3:
                equipButtons[3].interactable = false; break;
            case 4:
                equipButtons[4].interactable = false; break;
        }
    }

    private void OwnedGunsStatus(object param)
    {
        string inv = UseProfile.OwnedGuns;
        if (inv.Contains("SMG"))
        {
            goToShopButtons[0].gameObject.SetActive(false);
        }
        else
        {
            goToShopButtons[0].gameObject.SetActive(true);
        }

        if (inv.Contains("Rifle"))
        {
            goToShopButtons[1].gameObject.SetActive(false);
        }
        else
        {
            goToShopButtons[1].gameObject.SetActive(true);
        }

        if (inv.Contains("Shotgun"))
        {
            goToShopButtons[2].gameObject.SetActive(false);
        }
        else
        {
            goToShopButtons[2].gameObject.SetActive(true);
        }

        if (inv.Contains("Sniper"))
        {
            goToShopButtons[3].gameObject.SetActive(false);
        }
        else
        {
            goToShopButtons[3].gameObject.SetActive(true);
        }
    }

    private void GoToShop()
    {
        Shop.Setup().Show();
    }

    private void EquipItem(int id)
    {
        UseProfile.EquippedGun = id;
        GamePlayController.Instance.playerContain.currentGun = id;
        GamePlayController.Instance.playerContain.handController.GunUpdate(id);
        InitState();
    }

    //public void BuyGun_1()
    //{
    //    if(UseProfile.Coin > 500)
    //    {
    //        UseProfile.Coin -= 500;
    //        EquipItem(1);
    //    }
    //    else
    //    {
    //        Debug.LogError("NotEnoghtCoin");
    //    }
    //}

}
