using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPanelSpawner : MonoBehaviour
{
    public List<GameObject> bonusPanels;

    public bool leftActive = false;
    public bool middleActive = false;
    public bool rightActive = false;

    public string leftBonusName;
    public float leftAdd;
    public float leftTotal;

    public string middleBonusName;
    public float middleAdd;
    public float middleTotal;

    public string rightBonusName;
    public float rightAdd;
    public float rightTotal;

    void Start()
    {
        if (leftActive)
        {
            bonusPanels[0].SetActive(true);
            BonusPanel leftPanel = bonusPanels[0].GetComponent<BonusPanel>();
            leftPanel.name = leftBonusName;
            leftPanel.add = leftAdd;
            leftPanel.total = leftTotal;
        }

        if (middleActive)
        {
            bonusPanels[1].SetActive(true);
            BonusPanel middlePanel = bonusPanels[1].GetComponent<BonusPanel>();
            middlePanel.name = middleBonusName;
            middlePanel.add = middleAdd;
            middlePanel.total = middleTotal;
        }

        if (rightActive)
        {
            bonusPanels[2].SetActive(true);
            BonusPanel rightPanel = bonusPanels[2].GetComponent<BonusPanel>();
            rightPanel.name = rightBonusName;
            rightPanel.add = rightAdd;
            rightPanel.total = rightTotal;
        }
    }

    void Update()
    {
        
    }
}
