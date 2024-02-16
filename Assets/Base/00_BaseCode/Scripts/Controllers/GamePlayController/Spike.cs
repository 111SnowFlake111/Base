using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int hitLimit = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hitLimit > 0)
        {
            GamePlayController.Instance.playerContain.isHurt = true;
            GamePlayController.Instance.playerContain.isMoving = false;
            hitLimit--;
        }
    }
}
