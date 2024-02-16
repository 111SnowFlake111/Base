using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSpawner : MonoBehaviour
{
    public List<GameObject> cylinders;

    public bool leftActive = false;
    public bool middleActive = false;
    public bool rightActive = false;

    void Start()
    {
        if (leftActive)
        {
            cylinders[0].SetActive(true);
        }

        if (middleActive)
        {
            cylinders[1].SetActive(true);
        }

        if (rightActive)
        {
            cylinders[2].SetActive(true);
        }
    }


    void Update()
    {
        
    }
}
