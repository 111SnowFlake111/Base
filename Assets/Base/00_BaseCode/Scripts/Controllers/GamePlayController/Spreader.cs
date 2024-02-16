using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spreader : MonoBehaviour
{
    public GameObject bulletTurret;

    private void Start()
    {
        SimplePool2.Preload(bulletTurret, 30);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet"))
        {
            SimplePool2.Despawn(other.gameObject);

            if (gameObject.tag == "StraightStraight")
            {
                SimplePool2.Spawn(bulletTurret, gameObject.transform.position + new Vector3(-0.2f, 0, 0), Quaternion.identity);
                SimplePool2.Spawn(bulletTurret, gameObject.transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
            }

            if (gameObject.tag == "StraightLeft")
            {
                SimplePool2.Spawn(bulletTurret, gameObject.transform.position + new Vector3(-0.2f, 0, 0), Quaternion.identity);
                SimplePool2.Spawn(bulletTurret, gameObject.transform.position + new Vector3(0.2f, 0, 0), new Quaternion(0, -45f, 0, 0));
            }

            if (gameObject.tag == "StraightRight")
            {
                SimplePool2.Spawn(bulletTurret, gameObject.transform.position + new Vector3(-0.2f, 0, 0), Quaternion.identity);
                SimplePool2.Spawn(bulletTurret, gameObject.transform.position + new Vector3(0.2f, 0, 0), new Quaternion(0, 45f, 0, 0));
            }

            if (gameObject.tag == "LeftRight")
            {
                SimplePool2.Spawn(bulletTurret, gameObject.transform.position + new Vector3(0.2f, 0, 0), new Quaternion(0, -45f, 0, 0));
                SimplePool2.Spawn(bulletTurret, gameObject.transform.position + new Vector3(0.2f, 0, 0), new Quaternion(0, 45f, 0, 0));
            }
        }
    }
}
