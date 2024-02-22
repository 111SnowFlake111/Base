using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLane : MonoBehaviour
{
    public GameObject gate3LanesMain;
    public GameObject gateMain;

    bool rewardReceived = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !rewardReceived)
        {
            //Cho Gate có 3 cửa
            if (gate3LanesMain != null)
            {
                if (gameObject.tag == "Lane1")
                {
                    GamePlayController.Instance.playerContain.bonusDamage += (gate3LanesMain.GetComponent<Gate3Lanes>().pointsForFirstLane / 100);
                    gate3LanesMain.GetComponent<Gate3Lanes>().DespawnBulletGate(1);
                    rewardReceived = true;
                }

                if (gameObject.tag == "Lane2")
                {
                    GamePlayController.Instance.playerContain.bonusDamage += (gate3LanesMain.GetComponent<Gate3Lanes>().pointsForSecondLane / 100);
                    gate3LanesMain.GetComponent<Gate3Lanes>().DespawnBulletGate(2);
                    rewardReceived = true;
                }

                if (gameObject.tag == "Lane3")
                {
                    GamePlayController.Instance.playerContain.bonusDamage += (gate3LanesMain.GetComponent<Gate3Lanes>().pointsForThirdLane / 100);
                    gate3LanesMain.GetComponent<Gate3Lanes>().DespawnBulletGate(3);
                    rewardReceived = true;
                }
            }           

            //Cho Gate có 1 cửa
            if (gateMain != null)
            {
                GamePlayController.Instance.playerContain.bonusDamage += (gateMain.GetComponent<Gate>().pointsForLane / 100);
                gateMain.GetComponent<Gate>().DespawnBulletGate();
                rewardReceived = true;
            }
            
            GamePlayController.Instance.gameScene.InitState();
        }        
    }
}
