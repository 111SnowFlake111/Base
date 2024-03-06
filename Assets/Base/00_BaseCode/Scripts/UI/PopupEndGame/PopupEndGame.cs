using Sirenix.Serialization.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PopupEndGame : BaseBox
{
    public static PopupEndGame instance;
    public static PopupEndGame Setup(bool smallReward = false, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<PopupEndGame>(PathPrefabs.END_GAME_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button noThanksBtn;
    public Button claimRewardBtn;
    public GachaBonus gacha;

    private void Init()
    {
        noThanksBtn.onClick.AddListener(delegate { ToEndGameUpgradeScreen(); });
        claimRewardBtn.onClick.AddListener(delegate { HandleClaimRW(); });
        gacha.Init();
    }
    private void InitState()
    {
        gacha.InitState();
    }
    private void HandleClaimRW()
    {
        bool rewardClaimed = false;

        gacha.ArrowStop();

        GameController.Instance.admobAds.ShowVideoReward(delegate { ClaimRW(); }, delegate { NotLoadVideo(); gacha.ArrowResume(); }, delegate { RewardIsClaimed(); }, ActionWatchVideo.ClaimMoneyMultiplier, UseProfile.GameLevel.ToString());

        void ClaimRW()
        {
            rewardClaimed = true;
            gacha.ReceiveMoneyReward();
            claimRewardBtn.interactable = false;
            noThanksBtn.interactable = false;
            //StartCoroutine(WaitForScreenChange());
        }

        void NotLoadVideo()
        {
            GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                    (             
                    claimRewardBtn.transform.position,
                    "No video at the moment!",
                    Color.white,
                    isSpawnItemPlayer: true,
                    false,
                    null
                    );
        }

        void RewardIsClaimed()
        {
            if (!rewardClaimed)
            {
                gacha.ArrowResume();
            }
            else
            {
                gacha.ArrowStop();
            }
        }
     
    }

    private void ToEndGameUpgradeScreen()
    {     
        gacha.canRun = false;
        Close();
        PopupEndGame_UpgradeScreen.Setup().Show();     
    }

    public IEnumerator WaitForScreenChange()
    {
        gacha.canRun = false;
        yield return new WaitForSeconds(1);
     
        Close();
        PopupEndGame_UpgradeScreen.Setup().Show();
        //SceneManager.LoadScene("GamePlay");
        //UseProfile.GameLevel++;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
        UseProfile.GameLevel++;
    }
}
