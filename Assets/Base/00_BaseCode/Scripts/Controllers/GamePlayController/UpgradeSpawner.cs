using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public List<GameObject> upgradeList;
    public List<GameObject> spawnPos;

    public int left = -1;
    public int middle = -1;
    public int right = -1;

    void Start()
    {
        if (left >= 0)
        {
            GameObject leftUpgrade = Instantiate(upgradeList[left]);
            leftUpgrade.transform.SetParent(spawnPos[0].transform, false);
        }

        if (middle >= 0)
        {
            GameObject middleUpgrade = Instantiate(upgradeList[middle]);
            middleUpgrade.transform.SetParent(spawnPos[1].transform, false);
        }

        if (right >= 0)
        {
            GameObject rightUpgrade = Instantiate(upgradeList[right]);
            rightUpgrade.transform.SetParent(spawnPos[2].transform, false);
        }
    }

    void Update()
    {
        
    }
}
