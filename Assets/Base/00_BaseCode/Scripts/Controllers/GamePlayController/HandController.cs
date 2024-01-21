﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{   
    public Bullet bullet;

    public GameObject handPlayer;    
    public GameObject bulletSpawner;

    private Vector3 firstPost;
    private Vector3 secondPost;

    public Transform leftLimit;
    public Transform rightLimit;

    public Camera camera;


    public void Start()
    {
        SimplePool2.Preload(bullet.gameObject, 10);

        HandleSpawnBullet();
    }
    public void Update()
    {
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
                    Debug.Log("inputed");
                }
            }
        }

    }
    public void HandleSpawnBullet()
    {
        var temp = SimplePool2.Spawn(bullet, bulletSpawner.transform.position, Quaternion.identity);
        temp.transform.localEulerAngles = new Vector3(78.6168823f, 0, 0);
        StartCoroutine(temp.HandleDestoy_2());
    }


    public void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Rock")
        {
            Debug.LogError("collided");
            var moveBackward = target.transform.position - new Vector3(0f, 0f, 5f);
            handPlayer.transform.DOLocalMoveZ(moveBackward.z, 0.25f).OnComplete(() =>
            {
                var die = target.transform.localEulerAngles + new Vector3(0, 0, 90);
                handPlayer.transform.DOLocalRotate(die, 0.25f);
            });
        }
    }
}

    
