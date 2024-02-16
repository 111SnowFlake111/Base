using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.position += new Vector3(3f, 0) * Time.deltaTime;
    }
}
