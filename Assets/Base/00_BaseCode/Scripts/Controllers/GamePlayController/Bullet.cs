using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bullet : MonoBehaviour
{
    public float inaccuracy = 0;
    private bool spawnCheck = false;

    public float damage = 1;
    public int cylinderDamage = 1;
    public int wallDamage = 1;

    public ParticleSystem hitEffect;

    float inaccuracyRange;

    void Update()
    {
        
        if (!spawnCheck && inaccuracy != 0)
        {
            inaccuracyRange = RandomX();
        }
        gameObject.transform.position += new Vector3(inaccuracyRange, 0, 50f) * Time.deltaTime;
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
        spawnCheck = true;
        return Random.Range(-inaccuracy, inaccuracy);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("End"))
        {
            SimplePool2.Despawn(gameObject);
        }

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
