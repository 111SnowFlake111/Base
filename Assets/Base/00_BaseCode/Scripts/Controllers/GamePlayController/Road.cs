using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Transform roadPos;

    private void Start()
    {

    }

    void Update()
    {
        if (GamePlayController.Instance.playerContain.isHurt)
        {
            StartCoroutine(PushBack());
        }

        if (GamePlayController.Instance.playerContain.isMoving && GamePlayController.Instance.playerContain.isAlive)
        { 
            roadPos.transform.position += new Vector3(0, 0, -15f) * Time.deltaTime;
        }
    }

    public IEnumerator PushBack()
    {
        roadPos.transform.position += new Vector3(0, 0, 0.5f);
        //var newPosForPlayer = roadPos.transform.position += new Vector3(0, 0, 0.5f);
        //roadPos.DOMoveZ(newPosForPlayer, 0.3f);
        yield return new WaitForSecondsRealtime(0.1f);
        GamePlayController.Instance.playerContain.isMoving = true;
        GamePlayController.Instance.playerContain.isHurt = false;
    }
    public IEnumerator RoadDestroyer()
    {
     
        yield return new WaitForSeconds(60);        
        SimplePool2.Despawn(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "DespawnBox")
        {
            SimplePool2.Despawn(gameObject);
        }
    }
}
