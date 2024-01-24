using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Gate : MonoBehaviour
{
    //public GameObject messageBox;
    //public TMP_Text message;
    public List<GameObject> bulletGateObject;
    //private GameObject GateBullet
    //{
    //    get
    //    {
    //        foreach (GameObject obj in bulletGateObject)
    //        {
    //            if(!obj.activeSelf)
    //            {
    //                return obj;
    //            }
    //        }
    //        return null;
    //    }
    //}    
    static int limit = 1;
    static int currentPoint = 0;
    private bool doorActive = false;

    void Start()
    {
        foreach (GameObject obj in bulletGateObject)
        {
            obj.SetActive(false);
        }
    }


    void Update()
    {
        if (doorActive)
        {
            foreach (GameObject obj in bulletGateObject)
            {
                obj.transform.position = gameObject.transform.position;
            }
        }

    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            //if (currentPoint >= 10)
            //{
            //    message.text = "+10 Points";
            //    var notif = Instantiate(messageBox, collider.transform);
            //    var flyingMessage = notif.transform.localPosition + new Vector3(0, 5, 0);
            //    notif.transform.DOLocalMoveY(flyingMessage.y, 3f).OnComplete(() => Destroy(notif));
            //}
            //else
            //{
            //    message.text = "+" + currentPoint.ToString() + " Points";
            //    var notif = Instantiate(messageBox, collider.transform);
            //    var flyingMessage = notif.transform.localPosition + new Vector3(0, 5, 0);
            //    notif.transform.DOLocalMoveY(flyingMessage.y, 3f).OnComplete(() => Destroy(notif));
            //}
            Debug.LogError("+" + (currentPoint).ToString() + " Points");
            limit = 0;
            currentPoint = 0;
            foreach (GameObject obj in bulletGateObject)
            {
                obj.SetActive(false);
            }
        }
    }

    public IEnumerator GateDestroyer()
    {
        yield return new WaitForSeconds(10);
        SimplePool2.Despawn(gameObject);
    }

    public void PointUpdate(int number)
    {
        if (number >= 4 && number < 8)
        {
            limit += 1;
        } else if (number == 8)
        {
            limit += 2;
        } else
        {
            Debug.LogError(currentPoint);
            return;
        }
        Debug.LogError(currentPoint);
        DoorUpdate();
    }

    public void DoorUpdate()
    {
        doorActive = true;
        for (int i = currentPoint; i < limit; i++)
        {            
            if (currentPoint >= 10)
            {
                break;
            }
            bulletGateObject[i].SetActive(true);
            Debug.LogError(i);
            //var newPosForBullet = gateBullet.transform.position + new Vector3(0.55f * (i + 1), 0, 0);
            //gateBullet.transform.DOMoveX(newPosForBullet.x, 0.25f);
            currentPoint++;
        }
    }
}
