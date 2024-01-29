using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Bullet bulletScript;

    public List<GameObject> gun;
    public List<GameObject> bulletType;

    public GameObject bullet;

    public GameObject handPlayer;
    public GameObject bulletSpawner;

    private Vector3 firstPost;
    private Vector3 secondPost;

    public Transform leftLimit;
    public Transform rightLimit;

    public Camera camera;
    public float baseRange = 1;
    public int currentGun = 0;

    public void Start()
    {
        foreach (GameObject bul in bulletType)
        {
            SimplePool2.Preload(bul, 30);
        }

        //Súng default = pistol

        //gun[0].SetActive(true);
        //bullet = bulletType[0];

        currentGun = 0;
        //gunSpeed = GetComponent<Animator>();

        switch (currentGun)
        {
            case 0:
                gun[0].SetActive(true);
                bullet = bulletType[0];
                baseRange = 1;
                break;
            case 1:
                gun[1].SetActive(true);
                bullet = bulletType[0];
                baseRange = 1;
                break;
            case 2:
                gun[2].SetActive(true);
                bullet = bulletType[1];
                baseRange = 1.25f;
                break;
            case 3:
                gun[3].SetActive(true);
                bullet = bulletType[2];
                baseRange = 0.5f;
                break;
            case 4:
                gun[4].SetActive(true);
                bullet = bulletType[3];
                baseRange = 1.75f;
                break;
            default:
                gun[0].SetActive(true);
                bullet = bulletType[0];
                baseRange = 1;
                break;
        }

        //GunUpdate(currentGun);
        bulletScript = bullet.GetComponent<Bullet>();
        HandleSpawnBullet();
    }
    public void Update()
    {
        //gunSpeed.speed = 1f;

        if (GamePlayController.Instance.playerContain.isAlive)
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
        }

    }
    public void HandleSpawnBullet()
    {
        var temp = SimplePool2.Spawn(bullet, bulletSpawner.transform.position, Quaternion.identity).GetComponent<Bullet>();
        temp.transform.localEulerAngles = new Vector3(78.6168823f, 0, 0);
        StartCoroutine(temp.HandleDestoy(baseRange));
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

        switch (ID)
        {
            case 0:
                currentGun = 0;
                gunID = 0;
                bulletID = 0;
                baseRange = 1;
                break;
            case 1:
                currentGun = 1;
                gunID = 1;
                bulletID = 0;
                baseRange = 1;
                break;
            case 2:
                currentGun = 2;
                gunID = 2;
                bulletID = 1;
                baseRange = 1.25f;
                break;
            case 3:
                currentGun = 3;
                gunID = 3;
                bulletID = 2;
                baseRange = 0.5f;
                break;
            case 4:
                currentGun = 4;
                gunID = 4;
                bulletID = 3;
                baseRange = 1.75f;
                break;
            default:
                currentGun = 0;
                gunID = 0;
                bulletID = 0;
                baseRange = 1;
                break;
        }

        gun[gunID].SetActive(true);
        bullet = bulletType[bulletID];
    }
}

