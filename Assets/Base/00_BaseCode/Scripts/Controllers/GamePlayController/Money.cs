using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int minValue = 40;
    public int maxValue = 60;

    public int finalValue;

    private void Start()
    {
        finalValue = Random.Range(minValue, maxValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UseProfile.Money += finalValue;
            Destroy(gameObject);
        }
    }
}
