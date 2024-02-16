using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GamePlayController.Instance.playerContain.victory = true;
            GamePlayController.Instance.playerContain.isMoving = false;
        }
    }
}
