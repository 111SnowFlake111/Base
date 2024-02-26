using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spreader : MonoBehaviour
{
    public GameObject bulletTurret;

    public GameObject bulletSpawnPointL;
    public GameObject bulletSpawnPointR;

    public bool straightStraight = false;
    public bool straightLeft = false;
    public bool straightRight = false;
    public bool leftRight = false;

    public bool customFiringAngle = false;
    public float rotationAngleL = 0;
    public float rotationAngleR = 0;

    private void Start()
    {
        SimplePool2.Preload(bulletTurret, 40);

        if (customFiringAngle)
        {
            bulletSpawnPointL.transform.rotation = Quaternion.Euler(0, -rotationAngleL, 0);
            bulletSpawnPointR.transform.rotation = Quaternion.Euler(0, rotationAngleR, 0);
        }
        else
        {
            if (straightLeft)
            {
                bulletSpawnPointL.transform.rotation = Quaternion.Euler(0, -5f, 0);
            }

            if (straightRight)
            {
                bulletSpawnPointR.transform.rotation = Quaternion.Euler(0, 5f, 0);
            }

            if (leftRight)
            {
                bulletSpawnPointR.transform.rotation = Quaternion.Euler(0, 5f, 0);
                bulletSpawnPointL.transform.rotation = Quaternion.Euler(0, -5f, 0);
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet"))
        {          
            SimplePool2.Despawn(other.gameObject);

            var temp1 = SimplePool2.Spawn(bulletTurret, bulletSpawnPointL.transform.position, bulletSpawnPointL.transform.rotation);
            StartCoroutine(temp1.GetComponent<BulletTurret>().DespawnBullet());

            var temp2 = SimplePool2.Spawn(bulletTurret, bulletSpawnPointR.transform.position, bulletSpawnPointR.transform.rotation);
            StartCoroutine(temp2.GetComponent<BulletTurret>().DespawnBullet());
        }

        if (other.tag == "BehindThePlayer")
        {
            Destroy(gameObject);
        }
    }
}
