using Org.BouncyCastle.Utilities.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cylinder : MonoBehaviour
{
    public Gate gate;
    public List<GameObject> bulletFilled;
    public GameObject bulletHole
    {
        get
        {
            foreach (GameObject bullet in bulletFilled)
            {
                if(!bullet.activeSelf)
                {
                    return bullet;
                }
            }
            return null;
        }
        
    }
    private int hitCount = 0;

    void Start()
    {
      
    }

    void Update()
    {
        gameObject.transform.position += new Vector3(0, 0, -40f) * Time.deltaTime;
    }

    public IEnumerator CylinderDestroyer()
    {
        yield return new WaitForSeconds(15);
        SimplePool2.Despawn(gameObject);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            if (hitCount < 8)
            {
                SimplePool2.Despawn(collider.gameObject);
                var temp = bulletHole;
                if(temp != null)
                {
                    bulletHole.SetActive(true);
                    hitCount++;
                    var temp1 = gameObject.transform.localEulerAngles + new Vector3(0, 0, 35);
                    gameObject.transform.DOLocalRotate(temp1, 0.15f).OnComplete
                        (() =>
                        {
                            if (hitCount == 8)
                            {
                                var moveToConveyor = new Vector3(-3.5f, -2.2f, 20f);
                                gameObject.transform.transform.DOLocalMove(moveToConveyor, 0.5f).OnComplete(() =>
                                {
                                    Debug.Log("Moving");
                                    gate.PointUpdate(hitCount);
                                    hitCount = 0;
                                    foreach (GameObject bullet in bulletFilled)
                                    {
                                        bullet.SetActive(false);
                                    }

                                    if (collider.gameObject.tag == "Gate" || collider.gameObject.tag == "LastGate")
                                    {
                                        SimplePool2.Despawn(gameObject);
                                    }
                                }
                                );
                            }
                        });
                }
            }
        }

        if (collider.tag == "BehindThePlayer")
        {
            var moveToConveyor = new Vector3(-3.5f, -2.2f, 20f);
            gameObject.transform.transform.DOLocalMove(moveToConveyor, 0.5f).OnComplete(() =>
            {
                Debug.Log("Moving");
                gate.PointUpdate(hitCount);
                hitCount = 0;
                foreach (GameObject bullet in bulletFilled)
                {
                    bullet.SetActive(false);
                }

                if (collider.gameObject.tag == "Gate" || collider.gameObject.tag == "LastGate")
                {
                    SimplePool2.Despawn(gameObject);
                }
            });
        }
    }
}
