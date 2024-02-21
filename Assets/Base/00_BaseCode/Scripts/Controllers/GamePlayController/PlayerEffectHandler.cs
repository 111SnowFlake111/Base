using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectHandler : MonoBehaviour
{
    public ParticleSystem powerUp;
    public ParticleSystem hurt;

    public Transform playerPos;

    private GameObject stonk;
    private GameObject ouchie;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (stonk != null)
        {
            stonk.transform.position = playerPos.position + new Vector3(0.2f, 0, 0);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Panel") || other.tag.Contains("Lane"))
        {
            stonk = Instantiate(powerUp.gameObject, playerPos.transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
            stonk.GetComponent<ParticleSystem>().Play();
            Destroy(stonk, 1f);
        }

        if (other.tag.Contains("Spike"))
        {
            if (other.GetComponent<Spike>().hitLimit > 0)
            {
                ouchie = Instantiate(hurt.gameObject, playerPos.transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
                ouchie.GetComponent<ParticleSystem>().Play();
                Destroy(ouchie, 1f);
            }           
        }
    }
}
