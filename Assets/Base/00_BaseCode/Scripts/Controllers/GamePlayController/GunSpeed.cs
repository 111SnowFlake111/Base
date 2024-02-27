using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpeed : MonoBehaviour
{
    public Animator gunSpeed;

    public float speed;

    void Start()
    {
        gunSpeed = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        gunSpeed.SetFloat("SpeedScale", speed + speed * GamePlayController.Instance.playerContain.bonusFireRate);
        //switch (GamePlayController.Instance.playerContain.currentGun)
        //{
        //    case 0:
        //        gunSpeed.SetFloat("SpeedScale", 1 + GamePlayController.Instance.playerContain.bonusFireRate);
        //        break;
        //    case 1:
        //        gunSpeed.SetFloat("SpeedScale", 2.25f + GamePlayController.Instance.playerContain.bonusFireRate);
        //        break;
        //    case 2:
        //        gunSpeed.SetFloat("SpeedScale", 1.75f + GamePlayController.Instance.playerContain.bonusFireRate);
        //        break;
        //    case 3:
        //        gunSpeed.SetFloat("SpeedScale", 0.75f + GamePlayController.Instance.playerContain.bonusFireRate);
        //        break;
        //    case 4:
        //        gunSpeed.SetFloat("SpeedScale", 0.25f + GamePlayController.Instance.playerContain.bonusFireRate);
        //        break;
        //}

        //timer += Time.deltaTime;
        //if (timer >= 3f)
        //{
        //    Debug.LogError(gunSpeed.speed);
        //    timer = 0;
        //}
    }
}
