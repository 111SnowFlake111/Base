using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContain : MonoBehaviour
{
    public MapController mapController;
    public HandController handController;
    public bool isAlive = true;
    public bool isMoving = false;
    public bool isHurt = false;
    public bool victory = false;

    public bool start = false;

    public float bonusFireRate = 0;
    public float bonusRange = 0;
    public float bonusDamage = 0;

    public int cash = 0;

    //public int rangeUpgradeCount = 0;
    //public int yearUpgradeCount = 0;
    //public int fireRateUpgradeCount = 0;

    public int currentGun;
    public float currentYear;
    //{
    //    get
    //    {
    //        return currentYear;
    //    }
    //    set
    //    {
    //        currentYear = value;
    //        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.LOCALYEARUPGRADE);
    //    }
    //}

    public bool doubleGun = false;
    public bool tripleGun = false;

    public void Start()
    {
        if (UseProfile.Year == 0)
        {
            UseProfile.Year = 1900;
        }

        bonusFireRate = 0.01f * UseProfile.FireRateUpgradeCount;
        bonusRange = 0.01f * UseProfile.RangeUpgradeCount;
        bonusDamage = 0.01f * UseProfile.DamageUpgradeCount;

        currentGun = Mathf.FloorToInt((UseProfile.Year - 1900) / 10);
        currentYear = UseProfile.Year;
        GamePlayController.Instance.playerContain.isMoving = false;

        GamePlayController.Instance.gameScene.InitState();

        this.RegisterListener(EventID.YEARUPGRADE, YearUpdate);
        this.RegisterListener(EventID.STATSUPGRADE, StatsUpdate);
    }

    public void RangeUp()
    {
        UseProfile.RangeUpgradeCount++;
        bonusRange += 0.01f;
    }

    public void YearUp()
    {
        UseProfile.Year++;
    }

    public void FireRateUp()
    {
        UseProfile.FireRateUpgradeCount++;
        bonusFireRate += 0.01f;
    }

    public void DamageUp()
    {
        UseProfile.DamageUpgradeCount++;
        bonusDamage += 0.01f;
    }


    //Event Listener Section
    public void YearUpdate(object dam)
    {
        currentGun = Mathf.FloorToInt((UseProfile.Year - 1900) / 10);
        currentYear = UseProfile.Year;
    }

    public void StatsUpdate(object dam)
    {
        bonusFireRate = 0.01f * UseProfile.FireRateUpgradeCount;
        bonusRange = 0.01f * UseProfile.RangeUpgradeCount;
        bonusDamage = 0.01f * UseProfile.DamageUpgradeCount;
    }
}
