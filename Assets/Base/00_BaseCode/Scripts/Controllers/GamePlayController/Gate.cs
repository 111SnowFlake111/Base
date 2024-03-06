using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Gate : MonoBehaviour
{
    public List<GameObject> bulletGateObject;
    public GameObject lane;
    public Transform postPos;

    int limit = 0;
    int currentPoint = 0;

    public int bulletsNumberForOnePoint = 2;
    public float pointsForLane = 10;

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
                if (UseProfile.GameLevel >= RemoteConfigController.GetIntConfig(FirebaseConfig.LEVEL_START_SHOW_SPECIAL_BUTTONS, 5))
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
        } else
        {
            limit += (number - 1) / bulletsNumberForOnePoint;
        }
        
        DoorUpdate();
    }

    public void DoorUpdate()
    {
        for (int i = currentPoint; i < limit; i++)
        {
            if (currentPoint >= 10)
            {
                break;
            }

            var temp = bulletGateObject[i].transform.position;
            bulletGateObject[i].SetActive(true);
            
            bulletGateObject[i].transform.position = postPos.position;
            bulletGateObject[i].transform.DOMoveX(temp.x, 0.25f);

            currentPoint++;
        }

        if (bulletGateObject[9].activeSelf && isActive)
        {
            lane.SetActive(true);
        }
    }

    public void DespawnBulletGate()
    {
        lane.SetActive(false);
        foreach (GameObject obj in bulletGateObject)
        {
            obj.SetActive(false);
        }
        isActive = false;
    }
}