using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bullet : MonoBehaviour
{
    public Transform posBullet;
   
    // Update is called once per frame
    void Update()
    {
        posBullet.transform.position += new Vector3(0, 0, 100f) * Time.deltaTime;
    }
    public IEnumerator HandleDestoy_2()
    {
        yield return new WaitForSeconds(1);
        //   Destroy(this.gameObject);
        SimplePool2.Despawn(this.gameObject); 
    }    
}
