using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spreader_v2 : MonoBehaviour
{
    public List<GameObject> bulletSpawnPoints;
    public GameObject bulletTurret;

    bool isHit = false;

    Vector3 ogScale;

    private void Start()
    {
        ogScale = transform.localScale;
    }

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

            transform.DOScale(new Vector3(ogScale.x * 1.1f, ogScale.y * 1.1f, ogScale.z), 0.1f)
                .OnComplete(() =>
                {
                    transform.DOScale(ogScale, 0.1f);
                });
        }

        if (other.tag == "BehindThePlayer")
        {
            Destroy(gameObject);
        }
    }
}
