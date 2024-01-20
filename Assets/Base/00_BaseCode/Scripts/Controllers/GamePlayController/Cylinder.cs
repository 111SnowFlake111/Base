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
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
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
                                var temp2 = gameObject.transform.localPosition + new Vector3(-5, 0, 0);
                                gameObject.transform.transform.DOLocalMoveX(temp2.x, 0.5f).OnComplete(() =>
                                {
                                    var temp3 = gameObject.transform.localPosition + new Vector3(0, 0, 50);
                                    gameObject.transform.transform.DOLocalMoveZ(temp3.z, 1f).OnComplete(() =>
                                    {
                                        gate.PointUpdate(hitCount);
                                        hitCount = 0;
                                        foreach (GameObject bullet in bulletFilled)
                                        {
                                            bullet.SetActive(false);
                                        }
                                        SimplePool2.Despawn(gameObject);
                                    });
                                }
                                );
                            }
                        });
                }
            }
        }

        if (collider.tag == "BehindThePlayer")
        {
            var moveToConveyor = gameObject.transform.localPosition + new Vector3(-5, 0, 0);
            gameObject.transform.transform.DOLocalMoveX(moveToConveyor.x, 0.5f).OnComplete(() =>
            {
                var moveToDoorPusher = gameObject.transform.localPosition + new Vector3(0, 0, 30);
                gameObject.transform.transform.DOLocalMoveZ(moveToConveyor.z, 1f).OnComplete(() =>
                {
                    
                    gate.PointUpdate(hitCount);
                    hitCount = 0;
                    foreach (GameObject bullet in bulletFilled)
                    {
                        bullet.SetActive(false);
                    }
                    SimplePool2.Despawn(gameObject);
                });
            });
        }
    }
}
