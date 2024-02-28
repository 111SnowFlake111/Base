using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using DG.Tweening;
using EventDispatcher;

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

    int currentGun;

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

        currentGun = GamePlayController.Instance.playerContain.currentGun;

        if (GamePlayController.Instance.playerContain.doubleGun)
        {
            handL = Instantiate(leftHands[currentGun], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
            handL.transform.parent = handPlayerBody.transform;

            handR = Instantiate(rightHands[currentGun], spawnPointsDual[1].transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;
        }
        else if (GamePlayController.Instance.playerContain.tripleGun)
        {
            handL = Instantiate(leftHands[currentGun], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
            handL.transform.parent = handPlayerBody.transform;

            handM = Instantiate(middleHands[currentGun], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
            handM.transform.parent = handPlayerBody.transform;

            handR = Instantiate(rightHands[currentGun], spawnPointsTriple[2].transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;
        }
        else
        {
            handR = Instantiate(rightHands[currentGun], spawnPointSingle.transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;
        }

        //this.RegisterListener(EventID.LOCALYEARUPGRADE, GunUpgradeChecker);
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

        if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10) != currentGun)
        {
            currentGun = Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10);
            GunUpdate(currentGun);
        }

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

    public void GunUpdate(int ID)
    {
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

        if (GamePlayController.Instance.playerContain.doubleGun)
        {
            handL = Instantiate(leftHands[ID], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
            handL.transform.parent = handPlayerBody.transform;

            handR = Instantiate(rightHands[ID], spawnPointsDual[1].transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;
        }
        else if (GamePlayController.Instance.playerContain.tripleGun)
        {
            handL = Instantiate(leftHands[ID], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
            handL.transform.parent = handPlayerBody.transform;

            handM = Instantiate(middleHands[ID], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
            handM.transform.parent = handPlayerBody.transform;

            handR = Instantiate(rightHands[ID], spawnPointsTriple[2].transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;
        }
        else
        {
            handR = Instantiate(rightHands[ID], spawnPointSingle.transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;
        }

        Debug.LogError("Updated, current gun is " + ID.ToString());

        currentGun = ID;
        handPlayer.transform.DORotate(new Vector3(0, 360f, 0), 1f);
    }

    public void GunUpgradeChecker(object dam)
    {
        if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10) != currentGun)
        {
            currentGun = Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10);
            GunUpdate(currentGun);
        }
    }
}

