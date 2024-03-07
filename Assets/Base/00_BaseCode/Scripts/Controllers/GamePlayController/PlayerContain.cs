using EventDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContain : MonoBehaviour
{
    public MapController mapController;
    public HandController handController;
    [NonSerialized] public bool isAlive = true;
    [NonSerialized] public bool isMoving = false;
    [NonSerialized] public bool isHurt = false;
    [NonSerialized] public bool victory = false;

    [NonSerialized] public bool start = false;

    [NonSerialized] public float bonusFireRate = 0;
    [NonSerialized] public float bonusRange = 0;
    [NonSerialized] public float bonusDamage = 0;

    [NonSerialized] public int cash = 0;

    //public int rangeUpgradeCount = 0;
    //public int yearUpgradeCount = 0;
    //public int fireRateUpgradeCount = 0;

    [NonSerialized] public int currentGun;
    [NonSerialized] public float currentYear;

    [NonSerialized] public float startingYear = 1850;

    [NonSerialized] public bool doubleGun = false;
    [NonSerialized] public bool tripleGun = false;

    public void Start()
    {
        currentYear = startingYear + (float)(UseProfile.YearUpgradeCount);

        bonusFireRate = 0.01f * UseProfile.FireRateUpgradeCount;
        bonusRange = 0.01f * UseProfile.RangeUpgradeCount;
        bonusDamage = 0.01f * UseProfile.DamageUpgradeCount;

        currentGun = Mathf.FloorToInt((currentYear - startingYear) / 10);

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
        UseProfile.YearUpgradeCount++;
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
        currentYear = startingYear + UseProfile.YearUpgradeCount;
        currentGun = Mathf.FloorToInt((currentYear - startingYear) / 10);       
    }

    public void StatsUpdate(object dam)
    {
        bonusFireRate = 0.01f * UseProfile.FireRateUpgradeCount;
        bonusRange = 0.01f * UseProfile.RangeUpgradeCount;
        bonusDamage = 0.01f * UseProfile.DamageUpgradeCount;
    }
}
