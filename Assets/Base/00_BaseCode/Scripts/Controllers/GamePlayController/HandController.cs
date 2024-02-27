using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using DG.Tweening;

public class HandController : MonoBehaviour
{
    public List<GameObject> rightHands;
    public List<GameObject> middleHands;
    public List<GameObject> leftHands;

    public GameObject spawnPointSingle;
    public List<GameObject> spawnPointsDual;
    public List<GameObject> spawnPointsTriple;

    public List<GameObject> bullets;

    public List<GameObject> gun;
    public List<GameObject> doubleGun;
    public List<GameObject> tripleGun;

    public List<GameObject> bulletType;

    public GameObject bullet;

    public GameObject handPlayer;
    public GameObject handPlayerBody;

    public GameObject bulletSpawner;
    public List<GameObject> bulletSpawnerDual;
    public List<GameObject> bulletSpawnerTriple;

    private Vector3 firstPost;
    private Vector3 secondPost;

    public Transform leftLimit;
    public Transform rightLimit;

    public bool speedUp = false;
    public bool speedDown = false;

    public Camera camera;
    public float baseRange = 1;

    GameObject handR;
    GameObject handM;
    GameObject handL;

    public void Start()
    {
        foreach (GameObject bul in bullets)
        {
            SimplePool2.Preload(bul, 50);
        }

        if (handR != null)
        {
            Destroy(handR);
        }

        if (handM != null)
        {
            Destroy(handM);
        }

        if (handL != null)
        {
            Destroy(handL);
        }
        
        handR = Instantiate(rightHands[0], spawnPointsDual[1].transform.position, Quaternion.identity);
        handR.transform.parent = handPlayerBody.transform;

        handL = Instantiate(leftHands[0], spawnPointsDual[0].transform.position, Quaternion.identity);
        handL.transform.parent = handPlayerBody.transform;


        //GunUpdate(GamePlayController.Instance.playerContain.currentGun);

        //Súng default = pistol

        //gun[0].SetActive(true);
        //bullet = bulletType[0];

        //currentGun = 0;
        //gunSpeed = GetComponent<Animator>();

        //GunUpdate(currentGun);
        //HandleSpawnBullet();
    }
    public void Update()
    {
        //gunSpeed.speed = 1f;
        //if (!GamePlayController.Instance.playerContain.start)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        GamePlayController.Instance.playerContain.isMoving = true;
        //        GamePlayController.Instance.playerContain.start = true;
        //        Debug.LogError("Player is Activated");
        //    }
        //}

        if (GamePlayController.Instance.playerContain.isAlive && GamePlayController.Instance.playerContain.start)
        {
            if (Input.GetMouseButtonDown(0)) // khi bấm chuột trái vào màn hình lần đầu tiên
            {
                firstPost = camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))// khi giữ chuột trái
            {
                secondPost = camera.ScreenToWorldPoint(Input.mousePosition);
                if (firstPost != secondPost)
                {
                    handPlayer.transform.position += new Vector3(secondPost.x - firstPost.x, 0, 0);
                    firstPost = secondPost;
                    if (handPlayer.transform.position.x <= leftLimit.position.x)
                    {
                        handPlayer.transform.position = new Vector3(leftLimit.position.x, handPlayer.transform.position.y, handPlayer.transform.position.z);
                    }
                    if (handPlayer.transform.position.x >= rightLimit.position.x)
                    {
                        handPlayer.transform.position = new Vector3(rightLimit.position.x, handPlayer.transform.position.y, handPlayer.transform.position.z);
                    }
                }
            }

            if (GamePlayController.Instance.playerContain.isMoving && GamePlayController.Instance.playerContain.isAlive)
            {
                if (speedUp)
                {
                    handPlayer.transform.position += new Vector3(0, 0, 13f) * Time.deltaTime;
                }
                else if (speedDown)
                {
                    handPlayer.transform.position += new Vector3(0, 0, 7f) * Time.deltaTime;
                }
                else
                {
                    handPlayer.transform.position += new Vector3(0, 0, 10f) * Time.deltaTime;
                }
            }
            
            if (GamePlayController.Instance.playerContain.isHurt)
            {
                handPlayer.transform.position += new Vector3(0, 0, -6f);
                GamePlayController.Instance.playerContain.isMoving = true;
                GamePlayController.Instance.playerContain.isHurt = false;
            }
        }

    }
    public void HandleSpawnBullet()
    {
        if (!GamePlayController.Instance.playerContain.doubleGun && !GamePlayController.Instance.playerContain.tripleGun)
        {
            var temp = SimplePool2.Spawn(bullet, bulletSpawner.transform.position, Quaternion.identity).GetComponent<Bullet>();
            temp.transform.localEulerAngles = new Vector3(78.6168823f, 0, 0);
            StartCoroutine(temp.HandleDestoy(baseRange));
        }
        
        if (GamePlayController.Instance.playerContain.doubleGun)
        {
            foreach(GameObject pos in bulletSpawnerDual)
            {
                var temp = SimplePool2.Spawn(bullet, pos.transform.position, Quaternion.identity).GetComponent<Bullet>();
                temp.transform.localEulerAngles = new Vector3(78.6168823f, 0, 0);
                StartCoroutine(temp.HandleDestoy(baseRange));
            }
        }

        if (GamePlayController.Instance.playerContain.tripleGun)
        {
            foreach(GameObject pos in bulletSpawnerTriple)
            {
                var temp = SimplePool2.Spawn(bullet, pos.transform.position, Quaternion.identity).GetComponent<Bullet>();
                temp.transform.localEulerAngles = new Vector3(78.6168823f, 0, 0);
                StartCoroutine(temp.HandleDestoy(baseRange));
            }
        }
    }

    public void GunUpdate(int ID)
    {
        /*
         * 0: Pistol
         * 1: SMG
         * 2: Rifle
         * 3: Shotgun
         * 4: Sniper
         */

        int gunID;
        int bulletID;

        foreach (GameObject obj in gun)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in doubleGun)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in tripleGun)
        {
            obj.SetActive(false);
        }

        switch (ID)
        {
            case 0:
                GamePlayController.Instance.playerContain.currentGun = 0;
                gunID = 0;
                bulletID = 0;
                baseRange = 1;
                break;
            case 1:
                GamePlayController.Instance.playerContain.currentGun = 1;
                gunID = 1;
                bulletID = 0;
                baseRange = 1;
                break;
            case 2:
                GamePlayController.Instance.playerContain.currentGun = 2;
                gunID = 2;
                bulletID = 1;
                baseRange = 1.25f;
                break;
            case 3:
                GamePlayController.Instance.playerContain.currentGun = 3;
                gunID = 3;
                bulletID = 2;
                baseRange = 0.5f;
                break;
            case 4:
                GamePlayController.Instance.playerContain.currentGun = 4;
                gunID = 4;
                bulletID = 3;
                baseRange = 1.75f;
                break;
            default:
                GamePlayController.Instance.playerContain.currentGun = 0;
                gunID = 0;
                bulletID = 0;
                baseRange = 1;
                break;
        }

        if (GamePlayController.Instance.playerContain.doubleGun)
        {
            doubleGun[gunID].SetActive(true);
        }
        else if (GamePlayController.Instance.playerContain.tripleGun)
        {
            tripleGun[gunID].SetActive(true);
        }
        else
        {
            gun[gunID].SetActive(true);
        }
        
        bullet = bulletType[bulletID];
    }
}

