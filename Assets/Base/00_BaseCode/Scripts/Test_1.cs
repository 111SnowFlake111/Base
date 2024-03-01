using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_1 : MonoBehaviour
{
    public static Test_1 Instance;

    public void Awake()
    {
        Instance = this;
    }
    public void HandleMethod()
    {
        //Debug.LogError("HandleMethod");
    }    
}
