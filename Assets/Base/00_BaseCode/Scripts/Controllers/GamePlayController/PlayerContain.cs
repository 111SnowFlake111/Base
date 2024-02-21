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

    public int money = 1000;
    public int rangeUpgradeCount = 0;
    public int damageUpgradeCount = 0;
    public int fireRateUpgradeCount = 0;

    public int currentGun;

    public bool doubleGun = false;
    public bool tripleGun = false;

    public void Start()
    {
        currentGun = UseProfile.EquippedGun;
        GamePlayController.Instance.playerContain.isMoving = false;
    }

    public void RangeUp()
    {
        rangeUpgradeCount++;
        bonusRange += 0.01f;
    }

    public void DamageUp()
    {
        damageUpgradeCount++;
        bonusDamage += 0.01f;
    }

    public void FireRateUp()
    {
        fireRateUpgradeCount++;
        bonusFireRate += 0.01f;
    }
}
