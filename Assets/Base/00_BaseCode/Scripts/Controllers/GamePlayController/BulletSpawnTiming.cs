using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnTiming : MonoBehaviour
{
    public void Shoot()
    {
        GamePlayController.Instance.playerContain.handController.HandleSpawnBullet();
    }
}
