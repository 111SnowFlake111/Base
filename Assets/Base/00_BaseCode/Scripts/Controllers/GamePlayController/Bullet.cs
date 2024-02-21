using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bullet : MonoBehaviour
{
    public float inaccuracy = 0;
    private bool spawnCheck = false;

    public Transform posBullet;
    public ParticleSystem hitEffect;

    void Update()
    {
        if (!spawnCheck)
        {
            inaccuracy = RandomX();
        }       
        posBullet.transform.position += new Vector3(inaccuracy, 0, 50f) * Time.deltaTime;
    }
    public IEnumerator HandleDestoy(float baseRange)
    {
        //Thời gian cho đạn bay tăng lên sẽ tương đương việc tăng Range
        yield return new WaitForSecondsRealtime(baseRange + baseRange * GamePlayController.Instance.playerContain.bonusRange);
        spawnCheck = false;
        SimplePool2.Despawn(gameObject);
    }

    public float RandomX()
    {
        if (GamePlayController.Instance.playerContain.currentGun == 1)
        {
            spawnCheck = true;
            return Random.Range(-2f, 2f);
        } 
        else if (GamePlayController.Instance.playerContain.currentGun == 2)
        {
            spawnCheck = true;
            return Random.Range(-1f, 1f);
        } 
        else
        {
            spawnCheck = true;
            return 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Rock") || other.tag.Contains("Cylinder") || other.tag.Contains("Panel") || other.tag.Contains("Straight") || other.tag.Contains("Wall"))
        {
            if (other.tag.Contains("Cylinder"))
            {
                var isHitable = other.GetComponent<Cylinder>().isHitAble;
                if (isHitable)
                {
                    GameObject hit = Instantiate(hitEffect.gameObject, posBullet.transform.position + new Vector3(0, 0, -2f), Quaternion.identity);
                    hit.GetComponent<ParticleSystem>().Play();
                    Destroy(hit, 0.5f);
                }
            }
            else
            {
                GameObject hit = Instantiate(hitEffect.gameObject, posBullet.transform.position + new Vector3(0, 0, -2f), Quaternion.identity);
                hit.GetComponent<ParticleSystem>().Play();
                Destroy(hit, 0.5f);
            } 
        }
    }
}
