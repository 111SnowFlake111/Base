using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : MonoBehaviour
{
    public ParticleSystem hitEffect;

    void Update()
    {
        gameObject.transform.position += transform.forward * Time.deltaTime * 30f;
    }

    public IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(2);
        SimplePool2.Despawn(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Rock") || other.tag.Contains("Cylinder") || other.tag.Contains("Panel") || other.tag.Contains("Straight") || other.tag.Contains("Wall") || other.tag.Contains("Spreader"))
        {
            if (other.tag.Contains("Cylinder"))
            {
                var isHitable = other.GetComponent<Cylinder>().isHitAble;
                if (isHitable)
                {
                    GameObject hit = Instantiate(hitEffect.gameObject, gameObject.transform.position + new Vector3(0, 0, -2f), Quaternion.identity);
                    hit.GetComponent<ParticleSystem>().Play();
                    Destroy(hit, 0.5f);
                }
            }
            else
            {
                GameObject hit = Instantiate(hitEffect.gameObject, gameObject.transform.position + new Vector3(0, 0, -2f), Quaternion.identity);
                hit.GetComponent<ParticleSystem>().Play();
                Destroy(hit, 0.5f);
            }
        }
    }
}
