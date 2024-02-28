using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOfSpikes : MonoBehaviour
{
    public List<GameObject> spikes;

    public int hitLimit = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hitLimit > 0)
        {
            GamePlayController.Instance.playerContain.isHurt = true;
            GamePlayController.Instance.playerContain.isMoving = false;
            GamePlayController.Instance.playerContain.currentYear -= 1f;
            GamePlayController.Instance.gameScene.InitState();
            hitLimit--;
        }
    }
}
