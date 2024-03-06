using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate3Lanes : MonoBehaviour
{
    public List<GameObject> bulletGateObject;
    public List<GameObject> lanes;
    public Transform postPos;

    int limit = 0;
    int currentPoint = 0;

    public int bulletsNumberForOnePoint = 2;

    public float pointsForFirstLane = 4;
    public float pointsForSecondLane = 8;
    public float pointsForThirdLane = 12;

    public bool allowTriggerSpecialUpgradeButtons = false;

    bool isActive = true;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Cylinder")
        {
            int points = collider.GetComponent<Cylinder>().hitCount;
            Destroy(collider.gameObject);
            PointUpdate(points);           
        }

        if (collider.tag == "Player")
        {
            if (allowTriggerSpecialUpgradeButtons)
            {
                if(UseProfile.GameLevel >= RemoteConfigController.GetIntConfig(FirebaseConfig.LEVEL_START_SHOW_SPECIAL_BUTTONS, 5))
                {
                    GamePlayController.Instance.gameScene.firstGatePassed = true;
                    GamePlayController.Instance.gameScene.InitState();
                }      
            }
        }
    }

    public void PointUpdate(int number)
    {
        if (number % bulletsNumberForOnePoint == 0)
        {
            limit += number / bulletsNumberForOnePoint;
        }
        else
        {
            limit += (number - 1) / bulletsNumberForOnePoint;
        }

        DoorUpdate();
    }

    public void DoorUpdate()
    {
        for (int i = currentPoint; i < limit; i++)
        {
            if (currentPoint >= 12)
            {
                break;
            }

            var temp = bulletGateObject[i].transform.position;
            bulletGateObject[i].SetActive(true);

            bulletGateObject[i].transform.position = postPos.position;
            bulletGateObject[i].transform.DOMoveX(temp.x, 0.25f);

            currentPoint++;
        }

        if (isActive)
        {
            if (bulletGateObject[3].activeSelf)
            {
                lanes[0].gameObject.SetActive(true);
            }

            if (bulletGateObject[7].activeSelf)
            {
                lanes[1].gameObject.SetActive(true);
            }

            if (bulletGateObject[11].activeSelf)
            {
                lanes[2].gameObject.SetActive(true);
            }
        }       
    }

    public void DespawnBulletGate(int num)
    {
        switch (num)
        {
            case 1:
                foreach (GameObject obj in lanes)
                {
                    obj.SetActive(false);
                }

                for (int i = 0; i < 4; i++)
                {
                    bulletGateObject[i].SetActive(false);
                }

                isActive = false;
                break;
            case 2:
                foreach (GameObject obj in lanes)
                {
                    obj.SetActive(false);
                }

                for (int i = 4; i < 8; i++)
                {
                    bulletGateObject[i].SetActive(false);
                }

                isActive = false;
                break;
            case 3:
                foreach (GameObject obj in lanes)
                {
                    obj.SetActive(false);
                }

                for (int i = 8; i < 12; i++)
                {
                    bulletGateObject[i].SetActive(false);
                }

                isActive = false;
                break;
        }
    }
}
