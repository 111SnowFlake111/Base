using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Transform roadPos;
    
    void Update()
    {
        roadPos.transform.position += new Vector3(0, 0, -50f) * Time.deltaTime;
    }

    public IEnumerator RoadDestroyer()
    {
     
        yield return new WaitForSeconds(60);
        
        SimplePool2.Despawn(gameObject);
    }
}
