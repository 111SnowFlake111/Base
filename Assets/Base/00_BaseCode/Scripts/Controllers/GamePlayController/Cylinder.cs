using Org.BouncyCastle.Utilities.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cylinder : MonoBehaviour
{
    //public List<GameObject> bulletFilled;

    public List<GameObject> bulletSmall;
    public List<GameObject> bulletMedium;
    public List<GameObject> bulletShotgun;
    public List<GameObject> bulletBig;

    public GameObject body;
    public GameObject playerDetector;

    public Transform leftLimit;
    public Transform rightLimit;

    public bool moveLR = false;
    public bool moveLFReverse = false;
    public float moveLRSpeed = 5f;
    bool moveLFCheck = false;

    public bool moveForward = false;
    public float moveForwardSpeed = 5f;

    public int hitCount = 0;
    private bool isOnConveyor = false;
    public bool isHitAble = true;

    public bool playerDetected = false;

    private void Start()
    {
        leftLimit = GamePlayController.Instance.playerContain.handController.leftLimit;
        rightLimit = GamePlayController.Instance.playerContain.handController.rightLimit;

        if (moveLR || moveForward)
        {
            playerDetector.SetActive(true);
        }

        //Phòng trường hợp nhập số âm
        if (moveLRSpeed < 0)
        {
            moveLRSpeed = Mathf.Abs(moveLRSpeed);
        }
    }

    void Update()
    {
        if(isHitAble && moveLR && playerDetected)
        {
            if (moveLFCheck)
            {
                if (moveLFReverse)
                {
                    gameObject.transform.position += new Vector3(-moveLRSpeed, 0, 0) * Time.deltaTime;
                }
                else
                {
                    gameObject.transform.position += new Vector3(moveLRSpeed, 0, 0) * Time.deltaTime;
                }                
            }
            else
            {
                if (moveLFReverse)
                {
                    gameObject.transform.position += new Vector3(moveLRSpeed, 0, 0) * Time.deltaTime;
                }
                else
                {
                    gameObject.transform.position += new Vector3(-moveLRSpeed, 0, 0) * Time.deltaTime;
                }
            }

            if (gameObject.transform.position.x <= leftLimit.position.x)
            {
                moveLFCheck = true;
            }

            if (gameObject.transform.position.x >= rightLimit.position.x)
            {
                moveLFCheck = false;
            }
        }

        if(isHitAble && moveForward && playerDetected)
        {
            gameObject.transform.position += new Vector3(0, 0, moveForwardSpeed) * Time.deltaTime;
        }

        if (isHitAble == false &&  isOnConveyor == false)
        {
            gameObject.transform.position += new Vector3(-20f, 0, 0) * Time.deltaTime;
        }

        if (isOnConveyor)
        {
            gameObject.transform.position += new Vector3(0, 0, 40f) * Time.deltaTime;
        }
    }

    public IEnumerator CylinderDestroyer()
    {
        yield return new WaitForSeconds(15);
        SimplePool2.Despawn(gameObject);
    }

    public void OnTriggerEnter(Collider collider)
    {
        var moveToConveyor = gameObject.transform.position + new Vector3(-15f, 0, 0);

        if (collider.gameObject.tag.Contains("Bullet") && isHitAble == true)
        {
            if (collider.gameObject.tag == "BulletSmall")
            {
                if (hitCount < 8)
                {
                    SimplePool2.Despawn(collider.gameObject);
                    bulletSmall[hitCount].SetActive(true);
                    hitCount++;
                    var temp1 = body.transform.localEulerAngles + new Vector3(0, 0, 40);
                    body.transform.DOLocalRotate(temp1, 0.1f).OnComplete
                        (() =>
                        {
                            if (hitCount >= 8)
                            {
                                hitCount = 8;
                                isHitAble = false;

                                //move = body.transform.DOMove(conveyorSpawner.position, 0.3f).OnComplete(() =>
                                //{
                                //    isOnConveyor = true;
                                //});
                            }
                        });
                    //var temp = bulletHole;

                    //if (temp != null)
                    //{
                        
                    //}
                }
            }

            if (collider.gameObject.tag == "BulletMedium")
            {
                if (hitCount < 8)
                {
                    SimplePool2.Despawn(collider.gameObject);
                    for (int i = 0; i < 2; i++)
                    {
                        if (hitCount >= 8)
                        {
                            break;
                        }
                        //var temp = bulletHole;
                        //bulletHole.SetActive(true);
                        bulletMedium[hitCount].SetActive(true);
                        hitCount++;
                    }
                    var temp1 = body.transform.localEulerAngles + new Vector3(0, 0, 80);
                    body.transform.DOLocalRotate(temp1, 0.1f).OnComplete
                        (() =>
                        {
                            if (hitCount >= 8)
                            {
                                hitCount = 8;
                                isHitAble = false;
                                
                                //move = body.transform.DOMove(conveyorSpawner.position, 0.3f).OnComplete(() =>
                                //{
                                //    isOnConveyor = true;
                                //});
                            }
                        }
                        );
                }
            }

            if (collider.gameObject.tag == "BulletShotgun")
            {
                if (hitCount < 8)
                {
                    SimplePool2.Despawn(collider.gameObject);
                    for (int i = 0; i < 4; i++)
                    {
                        if (hitCount >= 8)
                        {
                            break;
                        }
                        //var temp = bulletHole;
                        //bulletHole.SetActive(true);
                        bulletShotgun[hitCount].SetActive(true);
                        hitCount++;
                    }
                    var temp1 = body.transform.localEulerAngles + new Vector3(0, 0, 160);
                    body.transform.DOLocalRotate(temp1, 0.1f).OnComplete
                        (() =>
                        {
                            if (hitCount >= 8)
                            {
                                hitCount = 8;
                                isHitAble = false;
                                
                                //move = body.transform.DOMove(conveyorSpawner.position, 0.3f).OnComplete(() =>
                                //{
                                //    isOnConveyor = true;
                                //});
                            }
                        }
                        );
                }
            }

            if (collider.gameObject.tag == "BulletBig")
            {
                if (hitCount < 8)
                {
                    SimplePool2.Despawn(collider.gameObject);
                    for (int i = 0; i < 8; i++)
                    {
                        if (hitCount >= 8)
                        {
                            break;
                        }
                        //var temp = bulletHole;
                        bulletBig[hitCount].SetActive(true);
                        hitCount++;
                    }
                    var temp1 = body.transform.localEulerAngles + new Vector3(0, 0, 320);
                    body.transform.DOLocalRotate(temp1, 0.1f).OnComplete
                        (() =>
                        {
                            isHitAble = false;
                            
                            //move = body.transform.DOMove(conveyorSpawner.position, 0.3f).OnComplete(() =>
                            //{
                            //    isOnConveyor = true;
                            //});
                        }
                        );
                }
            }
        }

        if (collider.tag == "BehindThePlayer")
        {
            isHitAble = false;

            //move = body.transform.DOMove(conveyorSpawner.position, 0.3f).OnComplete(() =>
            //{
            //    isOnConveyor = true;
            //});
        }

        if (collider.tag == "Conveyor")
        {
            isOnConveyor = true;
        }

        //if (collider.tag == "Gate" || collider.tag == "LastGate")
        //{
        //    gate.PointUpdate(hitCount);
        //    hitCount = 0;
        //    foreach (GameObject bullet in bulletFilled)
        //    {
        //        bullet.SetActive(false);
        //    }
        //    isOnConveyor = false;
        //    isHitAble = true;
        //    SimplePool2.Despawn(gameObject);
        //}  
    }
}
