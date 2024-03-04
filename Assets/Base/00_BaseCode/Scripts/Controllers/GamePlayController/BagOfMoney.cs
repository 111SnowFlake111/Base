using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfMoney : MonoBehaviour
{
    public int minValue = 5000;
    public int maxValue = 10000;

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
            GamePlayController.Instance.playerContain.cash += finalValue;

            //Debug.LogError("Money gained: " + GamePlayController.Instance.playerContain.cash);
            gameObject.SetActive(false);
        }
    }
}
