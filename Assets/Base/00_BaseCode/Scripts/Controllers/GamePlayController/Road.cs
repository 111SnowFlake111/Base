using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Transform roadPos;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroyer", 8);
    }

    // Update is called once per frame
    void Update()
    {
        roadPos.transform.position += new Vector3(0, 0, -50f) * Time.deltaTime;
    }

    public void Destroyer()
    {
        Destroy(this.gameObject);
    }
}
