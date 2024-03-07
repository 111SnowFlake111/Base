using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EventDispatcher;
using UnityEditor.Experimental.GraphView;
using System;

public class HandController : MonoBehaviour
{
    public List<GameObject> rightHands;
    public List<GameObject> middleHands;
    public List<GameObject> leftHands;

    public List<GameObject> specialLeftHands;
    public List<GameObject> specialMiddleHands;

    public GameObject spawnPointSingle;
    public List<GameObject> spawnPointsDual;
    public List<GameObject> spawnPointsTriple;

    public List<GameObject> bullets;

    //public GameObject bullet;

    public GameObject handPlayer;
    public GameObject handPlayerBody;

    public Transform leftLimit;
    public Transform rightLimit;

    [NonSerialized] public bool speedUp = false;
    [NonSerialized] public bool speedDown = false;

    public Camera camera;

    [NonSerialized] public int currentGun;
    bool allowChangeGun;

    Vector3 firstPost;
    Vector3 secondPost;

    bool mousePosReceived = true;

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

    private void FixedUpdate()
    {
        //Điều khiển
        //Logic: Khi bấm sẽ ghi vị trí chuột vào firstPost. Khi giữ sẽ ghi vào secondPost.
            // Khi kéo thì sẽ cộng dồn vào vị trí ban đầu * offSet (để đẩy đi nhanh hơn) và gán firstPost = secondPost để tránh việc chạy cộng dồn liên tục. Nếu không gán bằng nhau thì việc điều khiển sẽ giống như FloatStick
            // Note: Nên đặt giá trị cộng dồn là tĩnh
        if (GamePlayController.Instance.playerContain.isAlive && GamePlayController.Instance.playerContain.start)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPost = camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))// khi giữ chuột trái
            {
                secondPost = camera.ScreenToWorldPoint(Input.mousePosition);
                if(secondPost.x == firstPost.x)
                {
                    return;
                }

                float _move = 0;
                if(secondPost.x - firstPost.x > 0)
                {
                    _move = .1f;
                }
                else
                {
                    _move = -.1f;
                }

                handPlayer.transform.localPosition = Vector3.Lerp(handPlayer.transform.localPosition, new Vector3(handPlayer.transform.localPosition.x + _move * 12f, 
                    handPlayer.transform.localPosition.y, handPlayer.transform.localPosition.z), 0.25f); //new Vector3((secondPost.x - firstPost.x) * 2f, 0, 0);                   
                //handPlayer.transform.position = new Vector3(handPlayer.transform.position.x + (secondPost.x - firstPost.x) * 5f, handPlayer.transform.position.y, handPlayer.transform.position.z);

                if (handPlayer.transform.localPosition.x <= leftLimit.localPosition.x)
                {
                    handPlayer.transform.localPosition = new Vector3(leftLimit.localPosition.x, handPlayer.transform.localPosition.y, handPlayer.transform.localPosition.z);
                }
                if (handPlayer.transform.localPosition.x >= rightLimit.localPosition.x)
                {
                    handPlayer.transform.localPosition = new Vector3(rightLimit.localPosition.x, handPlayer.transform.localPosition.y, handPlayer.transform.localPosition.z);
                }

                firstPost = secondPost;
            }
        }
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

        
        //if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10) < 0)
        //{
        //    currentGun = 0;
        //    GunUpdate(currentGun);
        //}
        //else if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - 1900) / 10) >= rightHands.Count)
        //{
        //    currentGun = rightHands.Count - 1;
        //    GunUpdate(currentGun);
        //}

        //Kiểm tra Year để thay súng
        if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - GamePlayController.Instance.playerContain.startingYear) / 10) < 0 || Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - GamePlayController.Instance.playerContain.startingYear) / 10) >= rightHands.Count)
        {
            allowChangeGun = false;
        }
        else
        {
            allowChangeGun = true;
        }

        if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - GamePlayController.Instance.playerContain.startingYear) / 10) != currentGun && allowChangeGun)
        {
            currentGun = Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - GamePlayController.Instance.playerContain.startingYear) / 10);
            GunUpdate(currentGun);
        }

        //Điều khiển
        

        // Di chuyển khi chạm các object thay đổi tốc độ
        if (GamePlayController.Instance.playerContain.isAlive && GamePlayController.Instance.playerContain.start)
        {

            if (GamePlayController.Instance.playerContain.isMoving && GamePlayController.Instance.playerContain.isAlive)
            {
                if (speedUp)
                {
                    handPlayer.transform.position += new Vector3(0, 0, 13f) * Time.deltaTime;
                }
                else if (speedDown)
                {
                    handPlayer.transform.position += new Vector3(0, 0, 5f) * Time.deltaTime;
                }
                else
                {
                    handPlayer.transform.position += new Vector3(0, 0, 9f) * Time.deltaTime;
                }
            }
            
            if (GamePlayController.Instance.playerContain.isHurt)
            {
                //handPlayer.transform.position += new Vector3(0, 0, -6f);
                handPlayer.transform.DOMove(new Vector3(handPlayer.transform.position.x, handPlayer.transform.position.y, handPlayer.transform.position.z - 5f), 0.1f).OnComplete(
                    () =>
                    {
                        GamePlayController.Instance.playerContain.isMoving = true;
                        GamePlayController.Instance.playerContain.isHurt = false;
                    });               
            }
        }

    }

    public void GunUpdate(int ID)
    {
        //if (handR != null)
        //{
        //    Destroy(handR);
        //}

        //if (handM != null)
        //{
        //    Destroy(handM);
        //}

        //if (handL != null)
        //{
        //    Destroy(handL);
        //}

        Destroy(handR);
        Destroy(handM);
        Destroy(handL);

        if (GamePlayController.Instance.playerContain.doubleGun)
        {
            handR = Instantiate(rightHands[ID], spawnPointsDual[1].transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;

            switch (UseProfile.SpecialGunLeftHand)
            {
                case StringHelper.DRAGUNOV:
                    handL = Instantiate(specialLeftHands[0], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M79:
                    handL = Instantiate(specialLeftHands[1], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.PKM:
                    handL = Instantiate(specialLeftHands[2], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.RPG7:
                    handL = Instantiate(specialLeftHands[3], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M240:
                    handL = Instantiate(specialLeftHands[4], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M72:
                    handL = Instantiate(specialLeftHands[5], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                default:
                    handL = Instantiate(leftHands[ID], spawnPointsDual[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
            }          
        }
        else if (GamePlayController.Instance.playerContain.tripleGun)
        {
            handR = Instantiate(rightHands[ID], spawnPointsTriple[2].transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;

            switch (UseProfile.SpecialGunLeftHand)
            {
                case StringHelper.DRAGUNOV:
                    handL = Instantiate(specialLeftHands[0], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M79:
                    handL = Instantiate(specialLeftHands[1], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.PKM:
                    handL = Instantiate(specialLeftHands[2], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.RPG7:
                    handL = Instantiate(specialLeftHands[3], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M240:
                    handL = Instantiate(specialLeftHands[4], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M72:
                    handL = Instantiate(specialLeftHands[5], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
                default:
                    handL = Instantiate(leftHands[ID], spawnPointsTriple[0].transform.localPosition, Quaternion.identity);
                    handL.transform.parent = handPlayerBody.transform;
                    break;
            }

            switch (UseProfile.SpecialGunMiddleHand)
            {
                case StringHelper.DRAGUNOV:
                    handM = Instantiate(specialMiddleHands[0], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
                    handM.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M79:
                    handM = Instantiate(specialMiddleHands[1], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
                    handM.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.PKM:
                    handM = Instantiate(specialMiddleHands[2], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
                    handM.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.RPG7:
                    handM = Instantiate(specialMiddleHands[3], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
                    handM.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M240:
                    handM = Instantiate(specialMiddleHands[4], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
                    handM.transform.parent = handPlayerBody.transform;
                    break;
                case StringHelper.M72:
                    handM = Instantiate(specialMiddleHands[5], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
                    handM.transform.parent = handPlayerBody.transform;
                    break;
                default:
                    handM = Instantiate(middleHands[ID], spawnPointsTriple[1].transform.localPosition, Quaternion.identity);
                    handM.transform.parent = handPlayerBody.transform;
                    break;
            }                   
        }
        else
        {
            handR = Instantiate(rightHands[ID], spawnPointSingle.transform.localPosition, Quaternion.identity);
            handR.transform.parent = handPlayerBody.transform;
        }

        handPlayerBody.transform.DORotate(new Vector3(0, 360f, 0), 0.5f, RotateMode.FastBeyond360).OnComplete(() =>
        {
            handPlayer.transform.eulerAngles = Vector3.zero;
        });
        currentGun = ID;

        //Debug.LogError("Gun Updated");
        
    }

    public void GunUpgradeChecker(object dam)
    {
        if (Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - GamePlayController.Instance.playerContain.startingYear) / 10) != currentGun)
        {
            currentGun = Mathf.FloorToInt((GamePlayController.Instance.playerContain.currentYear - GamePlayController.Instance.playerContain.startingYear) / 10);
            GunUpdate(currentGun);
        }
    }
}

