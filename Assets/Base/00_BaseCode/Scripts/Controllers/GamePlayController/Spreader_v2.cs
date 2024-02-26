using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spreader_v2 : MonoBehaviour
{
    public List<GameObject> bulletSpawnPoints;
    public GameObject bulletTurret;

    bool isHit = false;

    void Update()
    {
        if (isHit)
        {
            gameObject.transform.Rotate(0, 3f, 0);
        }
        else
        {
            gameObject.transform.Rotate(0, 1f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet"))
        {
            SimplePool2.Despawn(other.gameObject);
            isHit = true;

            foreach (GameObject obj in bulletSpawnPoints)
            {
                var temp = SimplePool2.Spawn(bulletTurret, obj.transform.position, obj.transform.rotation);
                StartCoroutine(temp.GetComponent<BulletTurret>().DespawnBullet());
            }
        }

        if (other.tag == "BehindThePlayer")
        {
            Destroy(gameObject);
        }
    }
}
