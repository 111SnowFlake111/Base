using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusPanel : MonoBehaviour
{
    public TMP_Text tvCount;
    private int count;
    
    //public Transform spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        count = 5;
        tvCount.text = count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position += new Vector3(0, 0, -40f) * Time.deltaTime;
        //if (road != null)
        //{
        //    gameObject.transform.position = road.transform.position;
        //}
    }

    public IEnumerator PanelDestroyer()
    {
        yield return new WaitForSeconds(15);
        SimplePool2.Despawn(gameObject);
    }

    public void OnTriggerEnter(UnityEngine.Collider collider)
    {
        if(collider.gameObject.tag.Contains("Bullet"))
        {
            SimplePool2.Despawn(collider.gameObject);
            count--;
            if (count == 0)
            {
                SimplePool2.Despawn(gameObject);
            }
            tvCount.text = count.ToString();
        }
    }
  
}
