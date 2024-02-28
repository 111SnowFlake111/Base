using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public GameObject obj;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (obj.tag == "Cylinder")
            {
                obj.GetComponent<Cylinder>().playerDetected = true;
            }

            if (obj.tag == "Panel")
            {
                obj.GetComponent<BonusPanel>().playerDetected = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (obj.tag == "Cylinder")
            {
                obj.GetComponent<Cylinder>().playerDetected = false;
            }

            if (obj.tag == "Panel")
            {
                obj.GetComponent<BonusPanel>().playerDetected = false;
            }
        }
    }
}
