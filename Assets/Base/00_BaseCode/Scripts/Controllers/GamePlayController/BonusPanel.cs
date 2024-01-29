using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusPanel : MonoBehaviour
{
    public List<Transform> panelParts;
    public List<Material> green;
    public List<Material> red;

    Renderer outter;
    Renderer inner;

    
    public TMP_Text bonusName;    
    public TMP_Text valueAdd;
    public TMP_Text finalValue;

    public string name;
    public float add;
    public float total;
    
    //public Transform spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        bonusName.text = name;
        valueAdd.text = add.ToString();
        finalValue.text = total.ToString();

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
    }

    public IEnumerator PanelDestroyer()
    {
        yield return new WaitForSeconds(15);
        SimplePool2.Despawn(gameObject);
    }

    public void OnTriggerEnter(UnityEngine.Collider collider)
    {
        if (collider.tag.Contains("Bullet"))
        {
            SimplePool2.Despawn(collider.gameObject);
            finalValue.text = (float.Parse(finalValue.text) + float.Parse(valueAdd.text)).ToString();
        }

        if (collider.tag == "Player")
        {
            if (bonusName.text.Contains("Rate"))
            {
                GamePlayController.Instance.playerContain.bonusFireRate += (float.Parse(finalValue.text) / 100);
            }

            if (bonusName.text.Contains("Range"))
            {
                GamePlayController.Instance.playerContain.bonusRange += (float.Parse(finalValue.text) / 100);
            }

            Destroy(gameObject);
        }
    }
  
}
