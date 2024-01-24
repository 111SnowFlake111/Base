using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContain : MonoBehaviour
{
    public MapController mapController;
    public HandController handController;
    public bool isAlive = true;
    public bool isHurt = false;

    public float bonusFireRate = 0;
    public float bonusRange = 0;
    public float bonusDamage = 0;
    public void Init()
    {

    }
}
