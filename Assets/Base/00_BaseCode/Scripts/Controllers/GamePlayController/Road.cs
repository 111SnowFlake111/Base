using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Transform roadPos;

    private void Start()
    {
        //    player = GameObject.FindGameObjectWithTag("Player").GetComponent<HandController>;
    }

    void Update()
    {
        //if (GamePlayController.Instance.playerContain.isHurt)
        //{
        //    roadPos.transform.position += new Vector3(0, 0, 30f);
        //}

        if(GamePlayController.Instance.playerContain.isAlive)
        {
            roadPos.transform.position += new Vector3(0, 0, -20f) * Time.deltaTime;
        }
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
