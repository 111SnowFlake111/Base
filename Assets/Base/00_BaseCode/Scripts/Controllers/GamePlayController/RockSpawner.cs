using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public SetOfRocks rocksSet;
    public float rockHpMultipler = 1;

    void Start()
    {
        SetOfRocks rocks = rocksSet.GetComponent<SetOfRocks>();
        rocks.hpMultiplier = rockHpMultipler;
    }

    void Update()
    {
        
    }
}
