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

    public RawImage selectedItemImage;
    public List<Texture> specialGunsImages;

    public Text range, speed, damage, cylinderDamage, wallDamage, inaccuracy;

    public List<Button> itemBoxes;
    public List<Button> equipButtons;
    public List<Button> unequipButtons;

    public Text currentlyEquippedLeftHand;
    public Text currentlyEquippedMiddleHand;

    public Button close;

    int currentlySelectedItem_id;
    string currentlySelectedItem_name;

    private void Init()
    {
        close.onClick.AddListener(Close);

        itemBoxes[0].onClick.AddListener(delegate { ShowcaseUpdate(0, "Dragunov"); EquipItemStatus(currentlySelectedItem_name); UnequipItemStatus(); });
        itemBoxes[1].onClick.AddListener(delegate { ShowcaseUpdate(1, "M79"); EquipItemStatus(currentlySelectedItem_name); UnequipItemStatus(); });
        itemBoxes[2].onClick.AddListener(delegate { ShowcaseUpdate(2, "PKM"); EquipItemStatus(currentlySelectedItem_name); UnequipItemStatus(); });
        itemBoxes[3].onClick.AddListener(delegate { ShowcaseUpdate(3, "RPG7"); EquipItemStatus(currentlySelectedItem_name); UnequipItemStatus(); });
        itemBoxes[4].onClick.AddListener(delegate { ShowcaseUpdate(4, "M240"); EquipItemStatus(currentlySelectedItem_name); UnequipItemStatus(); });
        itemBoxes[5].onClick.AddListener(delegate { ShowcaseUpdate(5, "M72"); EquipItemStatus(currentlySelectedItem_name); UnequipItemStatus(); });

        equipButtons[0].onClick.AddListener(delegate { Equip(0); });
        equipButtons[1].onClick.AddListener(delegate { Equip(1); });

        unequipButtons[0].onClick.AddListener(delegate { Unequip(0); });
        unequipButtons[1].onClick.AddListener (delegate { Unequip(1); });

        InitState();

        this.RegisterListener(EventID.OWNEDSPECIALGUNSUPDATE, OwnedGunsStatus);
    }

    public void InitState()
    {
        OwnedGunsStatus(currentlySelectedItem_id);

        //ShowcaseUpdate(currentlySelectedItem_id, currentlySelectedItem_name);
        //EquipItemStatus(currentlySelectedItem_name);
        UnequipItemStatus();

        currentlyEquippedLeftHand.text = UseProfile.SpecialGunLeftHand;
        currentlyEquippedMiddleHand.text = UseProfile.SpecialGunMiddleHand;
    }

    private void Equip(int id)
    {
        switch (id)
        {
            case 0:
                UseProfile.SpecialGunLeftHand = currentlySelectedItem_name;
                break;
            case 1:
                UseProfile.SpecialGunMiddleHand = currentlySelectedItem_name;
                break;
        }

        EquipItemStatus(currentlySelectedItem_name);
        InitState();
    }

    private void Unequip(int id)
    {
        switch (id)
        {
            case 0:
                UseProfile.SpecialGunLeftHand = "";
                break;
            case 1:
                UseProfile.SpecialGunMiddleHand = "";
                break;
        }

        InitState();
    }

    private void ShowcaseUpdate(int id, string gunName)
    {
        currentlySelectedItem_id = id;
        currentlySelectedItem_name = gunName;

        selectedItemImage.GetComponent<RawImage>().texture = specialGunsImages[currentlySelectedItem_id];

        range.text = "Range:\n" + GamePlayController.Instance.playerContain.handController.specialLeftHands[currentlySelectedItem_id].GetComponent<BulletSpawnTiming>().baseRange.ToString();

        speed.text = "Speed:\n" + GamePlayController.Instance.playerContain.handController.specialLeftHands[currentlySelectedItem_id].GetComponent<GunSpeed>().speed.ToString();

        damage.text = "Damage:\n" + GamePlayController.Instance.playerContain.handController.specialLeftHands[currentlySelectedItem_id].GetComponent<BulletSpawnTiming>().bullet.GetComponent<Bullet>().damage.ToString();

        cylinderDamage.text = "Cylinder Damage:\n" + GamePlayController.Instance.playerContain.handController.specialLeftHands[currentlySelectedItem_id].GetComponent<BulletSpawnTiming>().bullet.GetComponent<Bullet>().cylinderDamage.ToString();

        wallDamage.text = "Wall Damage:\n" + GamePlayController.Instance.playerContain.handController.specialLeftHands[currentlySelectedItem_id].GetComponent<BulletSpawnTiming>().bullet.GetComponent<Bullet>().wallDamage.ToString();

        inaccuracy.text = "Inaccuracy:\n" + GamePlayController.Instance.playerContain.handController.specialLeftHands[currentlySelectedItem_id].GetComponent<BulletSpawnTiming>().bullet.GetComponent<Bullet>().inaccuracy.ToString();
    }

    private void EquipItemStatus(string gunName)
    {
        string inv = UseProfile.OwnedSpecialGuns;
        string leftHand = UseProfile.SpecialGunLeftHand;
        string middleHand = UseProfile.SpecialGunMiddleHand;

        if (inv.Contains(gunName))
        {
            if (leftHand.Contains(gunName))
            {
                equipButtons[0].interactable = false;
            }
            else
            {
                equipButtons[0].interactable = true;
            }

            if (middleHand.Contains(gunName))
            {
                equipButtons[1].interactable = false;
            }
            else
            {
                equipButtons[1].interactable = true;
            }
        }
    }

    private void UnequipItemStatus()
    {
        if (UseProfile.SpecialGunLeftHand.Length <= 0)
        {
            unequipButtons[0].gameObject.SetActive(false);
        }
        else
        {
            unequipButtons[0].gameObject.SetActive(true);
        }

        if (UseProfile.SpecialGunMiddleHand.Length <= 0)
        {
            unequipButtons[1].gameObject.SetActive(false);
        }
        else
        {
            unequipButtons[1].gameObject.SetActive(true);
        }
    }

    //Event Listener Section
    private void OwnedGunsStatus(object param)
    {
        string inv = UseProfile.OwnedSpecialGuns;

        if (inv.Contains("Dragunov"))
        {
            itemBoxes[0].interactable = true;
        }
        else
        {
            itemBoxes[0].interactable = false;
        }

        if (inv.Contains("M79"))
        {
            itemBoxes[1].interactable = true;
        }
        else
        {
            itemBoxes[1].interactable = false;
        }

        if (inv.Contains("PKM"))
        {
            itemBoxes[2].interactable = true;
        }
        else
        {
            itemBoxes[2].interactable = false;
        }

        if (inv.Contains("RPG7"))
        {
            itemBoxes[3].interactable = true;
        }
        else
        {
            itemBoxes[3].interactable = false;
        }

        if (inv.Contains("M240"))
        {
            itemBoxes[4].interactable = true;
        }
        else
        {
            itemBoxes[4].interactable = false;
        }

        if (inv.Contains("M72"))
        {
            itemBoxes[5].interactable = true;
        }
        else
        {
            itemBoxes[5].interactable = false;
        }
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
