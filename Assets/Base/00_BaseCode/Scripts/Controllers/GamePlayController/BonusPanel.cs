using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusPanel : MonoBehaviour
{
    public GameObject wall;

    public List<Transform> panelParts;
    public List<Material> green;
    public List<Material> red;

    Renderer outter;
    Renderer inner;
    
    public TMP_Text bonusName;    
    public TMP_Text valueAdd;
    public TMP_Text finalValue;

    public bool hasWall = false;
    public int wallHP = 5;
    public int wallHitLimit = 1;

    public string name;
    public float add = 1;       //Default: +1
    public float total = 0;     //Default: 0
    
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
                UseProfile.Money += (int.Parse(finalValue.text));
            }

            GamePlayController.Instance.gameScene.InitState();
            Destroy(gameObject);
        }
    }
  
}
