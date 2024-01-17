using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform posBullet;
    public void Start()
    {
        // Invoke("HandleDestroy",1); // đợi thời gian value rồi thực thi hàm
        StartCoroutine(HandleDestoy_2()); // thực thi ngay gặp yield rồi chờ thực thi tiếp
    }
    // Update is called once per frame
    void Update()
    {
        posBullet.transform.position += new Vector3(0, 0, 10f) * Time.deltaTime;
    }
    

    public void HandleDestroy()
    {
        Destroy(this.gameObject);
    }    


    public IEnumerator HandleDestoy_2()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);     
    }    
}
