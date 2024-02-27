using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnTiming : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bullet;

    public float baseRange = 1;
    public void Shoot()
    {
        //GamePlayController.Instance.playerContain.handController.HandleSpawnBullet();
        var temp = SimplePool2.Spawn(bullet, bulletSpawnPoint.transform.position, bullet.transform.rotation);
        StartCoroutine(temp.GetComponent<Bullet>().HandleDestoy(baseRange));
    }
}
