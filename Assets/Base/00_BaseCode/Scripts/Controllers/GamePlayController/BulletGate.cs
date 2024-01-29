using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGate : MonoBehaviour
{
    public Material redColor;

    public Renderer body;
    public Renderer bottom;
    void Start()
    {
        var gateTag = transform.parent.tag;
        if (gateTag.Contains("Last"))
        {
            body.material = redColor;
            bottom.material = redColor;
        }
    }

    void Update()
    {
        
    }
}
