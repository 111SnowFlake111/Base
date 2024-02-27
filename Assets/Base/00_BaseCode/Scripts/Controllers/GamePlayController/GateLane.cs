using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateLane : MonoBehaviour
{
    public GameObject gate3LanesMain;
    public GameObject gateMain;

    public TMP_Text gateLaneGate3Lanes;
    public TMP_Text gateLaneSingle;

    bool rewardReceived = false;

    private void Start()
    {
        if (gate3LanesMain != null)
        {
            if (gameObject.tag == "Lane1")
            {
                gateLaneGate3Lanes.text = "+ " + gate3LanesMain.GetComponent<Gate3Lanes>().pointsForFirstLane.ToString();
            }
            
            if (gameObject.tag == "Lane2")
            {
                gateLaneGate3Lanes.text = "+ " + gate3LanesMain.GetComponent<Gate3Lanes>().pointsForSecondLane.ToString();
            }
            
            if (gameObject.tag == "Lane3")
            {
                gateLaneGate3Lanes.text = "+ " + gate3LanesMain.GetComponent<Gate3Lanes>().pointsForThirdLane.ToString();
            }            
        }

        if (gateMain != null)
        {
            gateLaneSingle.text = "+ " + gateMain.GetComponent<Gate>().pointsForLane.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !rewardReceived)
        {
            //Cho Gate có 3 cửa
            if (gate3LanesMain != null)
            {
                if (gameObject.tag == "Lane1")
                {
                    GamePlayController.Instance.playerContain.currentYear += (gate3LanesMain.GetComponent<Gate3Lanes>().pointsForFirstLane);
                    gate3LanesMain.GetComponent<Gate3Lanes>().DespawnBulletGate(1);
                    rewardReceived = true;
                }

                if (gameObject.tag == "Lane2")
                {
                    GamePlayController.Instance.playerContain.currentYear += (gate3LanesMain.GetComponent<Gate3Lanes>().pointsForSecondLane);
                    gate3LanesMain.GetComponent<Gate3Lanes>().DespawnBulletGate(2);
                    rewardReceived = true;
                }

                if (gameObject.tag == "Lane3")
                {
                    GamePlayController.Instance.playerContain.currentYear += (gate3LanesMain.GetComponent<Gate3Lanes>().pointsForThirdLane);
                    gate3LanesMain.GetComponent<Gate3Lanes>().DespawnBulletGate(3);
                    rewardReceived = true;
                }
            }           

            //Cho Gate có 1 cửa
            if (gateMain != null)
            {
                //GamePlayController.Instance.playerContain.bonusDamage += (gateMain.GetComponent<Gate>().pointsForLane / 100);
                GamePlayController.Instance.playerContain.currentYear += gateMain.GetComponent<Gate>().pointsForLane;
                gateMain.GetComponent<Gate>().DespawnBulletGate();
                rewardReceived = true;
            }
            
            GamePlayController.Instance.gameScene.InitState();
        }        
    }
}
