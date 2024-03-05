using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BonusPanel : MonoBehaviour
{
    public GameObject wall;
    public GameObject playerDetector;

    public List<Transform> panelParts;
    public List<Material> green;
    public List<Material> red;

    Renderer outter;
    Renderer inner;

    Transform leftLimit;
    Transform rightLimit;

    public TMP_Text bonusName;    
    public TMP_Text valueAdd;
    public TMP_Text finalValue;

    public bool hasWall = false;
    public int wallHP = 5;
    public int wallHitLimit = 1;

    public bool moveLR = false;
    public bool moveLFReverse = false;
    public float moveLRSpeed = 5f;
    bool moveLFCheck = false;

    public bool moveForward = false;
    public float moveForwardSpeed = 5f;

    public bool moveTowardPlayer = false;
    public float moveTowardPlayerSpeed = 5f;

    public bool useCustomXLimit = false;
    public float customXLeft = -3f;
    public float customXRight = 3f;

    public bool useCustomZLimit = false;
    public float customZForward = 100f;

    public bool playerDetected = false;

    public string name;
    public float add = 1;       //Default: +1
    public float total = 0;     //Default: 0

    Vector3 ogScale;

    //public Transform spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        if (hasWall)
        {
            wall.SetActive(true);
        }

        bonusName.text = name;
        if (add < 0)
        {
            valueAdd.text = add.ToString();
            valueAdd.color = Color.red;
        }
        else
        {
            valueAdd.text = add.ToString();
        }
        
        if (total > 0)
        {
            finalValue.text = "+" + total.ToString();
        }
        else
        {
            finalValue.text = total.ToString();
        }

        leftLimit = GamePlayController.Instance.playerContain.handController.leftLimit;
        rightLimit = GamePlayController.Instance.playerContain.handController.rightLimit;

        if (moveLR || moveForward || moveTowardPlayer)
        {
            playerDetector.SetActive(true);
        }

        //Phòng trường hợp nhập số âm
        if (moveLRSpeed < 0)
        {
            moveLRSpeed = Mathf.Abs(moveLRSpeed);
        }

        if (moveLFReverse)
        {
            moveLFCheck = true;
        }

        ogScale = gameObject.transform.localScale;

        outter = panelParts[0].GetComponent<Renderer>();
        inner = panelParts[1].GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position += new Vector3(0, 0, -40f) * Time.deltaTime;
        //if (road != null)
        //{
        //    gameObject.transform.position = road.transform.position;
        //}
        if (float.Parse(finalValue.text) <= 0)
        {
            outter.material = red[0];
            inner.material = red[1];
        } 
        else
        {
            outter.material = green[0];
            inner.material = green[1];           
        }

        if (moveLR && playerDetected)
        {
            if (moveLFCheck)
            {
                gameObject.transform.position += new Vector3(moveLRSpeed, 0, 0) * Time.deltaTime;
            }
            else
            {
                gameObject.transform.position += new Vector3(-moveLRSpeed, 0, 0) * Time.deltaTime;
            }

            if (useCustomXLimit)
            {
                if (gameObject.transform.position.x <= customXLeft)
                {
                    moveLFCheck = true;
                }

                if (gameObject.transform.position.x >= customXRight)
                {
                    moveLFCheck = false;
                }
            }
            else
            {
                if (gameObject.transform.position.x <= leftLimit.position.x)
                {
                    moveLFCheck = true;
                }

                if (gameObject.transform.position.x >= rightLimit.position.x)
                {
                    moveLFCheck = false;
                }
            }
        }

        if (moveForward && playerDetected)
        {
            gameObject.transform.position += new Vector3(0, 0, moveForwardSpeed) * Time.deltaTime;

            if (useCustomZLimit)
            {
                if (gameObject.transform.position.z >= customZForward)
                {
                    moveForward = false;
                }
            }
        }

        if (moveTowardPlayer && playerDetected)
        {
            gameObject.transform.position += new Vector3(0, 0, -moveTowardPlayerSpeed) * Time.deltaTime;
        }
    }

    public IEnumerator PanelDestroyer()
    {
        yield return new WaitForSeconds(15);
        SimplePool2.Despawn(gameObject);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Contains("Bullet") && !hasWall)
        {
            SimplePool2.Despawn(collider.gameObject);
            if (total > 0)
            {
                finalValue.text = "+" + (float.Parse(finalValue.text) + float.Parse(valueAdd.text)).ToString();
            }
            else
            {
                finalValue.text = (float.Parse(finalValue.text) + float.Parse(valueAdd.text)).ToString();
            }
            
            transform.DOScale(new Vector3(ogScale.x * 1.1f, ogScale.y * 1.1f, ogScale.z), 0.1f)
                .OnComplete(() =>
                {
                    transform.DOScale(ogScale, 0.1f);
                });
        }

        if (collider.tag == "Player" && !hasWall)
        {
            if (bonusName.text.Contains("Rate"))
            {
                GamePlayController.Instance.playerContain.bonusFireRate += (float.Parse(finalValue.text) / 100);
            }

            if (bonusName.text.Contains("Range"))
            {
                GamePlayController.Instance.playerContain.bonusRange += (float.Parse(finalValue.text) / 100);
            }

            if (bonusName.text.Contains("Power"))
            {
                GamePlayController.Instance.playerContain.bonusDamage += (float.Parse(finalValue.text) / 100);
            }

            if (bonusName.text.Contains("Money"))
            {
                UseProfile.Money += Mathf.RoundToInt(float.Parse(finalValue.text));
            }

            if (bonusName.text.Contains("Year"))
            {
                GamePlayController.Instance.playerContain.currentYear += float.Parse(finalValue.text);
            }

            if (bonusName.text.Contains("Month"))
            {
                GamePlayController.Instance.playerContain.currentYear += float.Parse(finalValue.text) / 12;
            }

            if (bonusName.text.Contains("Day"))
            {
                GamePlayController.Instance.playerContain.currentYear += float.Parse(finalValue.text) / 365;
            }

            GamePlayController.Instance.gameScene.InitState();
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Bullet"))
        {
            SimplePool2.Despawn(other.gameObject);
        }
    }
}
