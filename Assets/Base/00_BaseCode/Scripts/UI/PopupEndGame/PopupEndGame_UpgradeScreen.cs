using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PopupEndGame_UpgradeScreen : BaseBox
{
    public static PopupEndGame_UpgradeScreen instance;
    public static PopupEndGame_UpgradeScreen Setup(bool smallReward = false, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<PopupEndGame_UpgradeScreen>(PathPrefabs.END_GAME_BOX_UPGRADE_SECTION));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Text rangeLevel;
    public Text damageLevel;

    public Text rangeCost;
    public Text damageCost;

    public Button rangeUpgradeBtn;
    public Button damageUpgradeBtn;
    public Button nextLevelBtn;

    private void Init()
    {
        rangeUpgradeBtn.onClick.AddListener(delegate { RangeUpgrade(); });
        damageUpgradeBtn.onClick.AddListener(delegate { DamageUpgrade(); });

        nextLevelBtn.onClick.AddListener(delegate { LoadNextLevel(); });

        InitState();
    }
    private void InitState()
    {
        rangeCost.text = (100 * (UseProfile.RangeUpgradeCount + 1)).ToString();
        damageCost.text = (100 * (UseProfile.DamageUpgradeCount + 1)).ToString();

        rangeLevel.text = "Level " + (UseProfile.RangeUpgradeCount + 1).ToString();
        damageLevel.text = "Level " + (UseProfile.DamageUpgradeCount + 1).ToString();

        if (UseProfile.Money < int.Parse(rangeCost.text))
        {
            rangeUpgradeBtn.interactable = false;
        }
        else
        {
            rangeUpgradeBtn.interactable = true;
        }

        if (UseProfile.Money < int.Parse(damageCost.text))
        {
            damageUpgradeBtn.interactable = false;
        }
        else
        {
            rangeUpgradeBtn.interactable = true;
        }
    }
    private void RangeUpgrade()
    {
        UseProfile.Money -= int.Parse(rangeCost.text);
        GamePlayController.Instance.playerContain.RangeUp();
        InitState();
        GamePlayController.Instance.gameScene.InitState();
    }

    private void DamageUpgrade()
    {
        UseProfile.Money -= int.Parse(damageCost.text);
        GamePlayController.Instance.playerContain.DamageUp();
        InitState();
        GamePlayController.Instance.gameScene.InitState();
    }

    private void LoadNextLevel()
    {
        NextLevel();
        
        //GameController.Instance.admobAds.ShowInterstitial(false, "NoThankEndGame", delegate { NextLevel(); } , UseProfile.GameLevel.ToString());
        void NextLevel()
        {
            SceneManager.LoadScene("GamePlay");
            UseProfile.GameLevel++;
        } 
    }
}
