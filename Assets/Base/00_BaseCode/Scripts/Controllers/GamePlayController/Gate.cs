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
    public Transform postPos;

    public int limit = 1;
    public int currentPoint = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
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
            if (currentPoint >= 10)
            {
                GamePlayController.Instance.playerContain.bonusDamage += 10;
            }
            else
            {
                GamePlayController.Instance.playerContain.bonusDamage += currentPoint;
            }
            limit = 1;
            currentPoint = 0;
            foreach (GameObject obj in bulletGateObject)
            {
                obj.SetActive(false);
            }
        }

        if (collider.tag == "Cylinder")
        {
            int points = collider.GetComponent<Cylinder>().hitCount;
            PointUpdate(points);
            collider.gameObject.SetActive(false);
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
            Debug.LogError("current point is: " + (currentPoint));
            return;
        }
        Debug.LogError("current point is: " + (currentPoint));
        DoorUpdate();
    }

    public void DoorUpdate()
    {
        for (int i = currentPoint; i < limit; i++)
        {            
            if (currentPoint >= 10)
            {
                break;
            }
            bulletGateObject[i].SetActive(true);
            bulletGateObject[i].transform.position = postPos.position;
            var newPosForBullet = postPos.position + new Vector3(0.45f + 0.55f * i, 0, 0);
            bulletGateObject[i].transform.DOMoveX(newPosForBullet.x, 0.25f);
            currentPoint++;
        }
    }
}
