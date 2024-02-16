using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOfRocks : MonoBehaviour
{
    public List<Rock> Rocks;

    [SerializeField]
    public float hpMultiplier = 1;

    void Start()
    {
        if (hpMultiplier <= 0)
        {
            hpMultiplier = 1;
        }

        foreach (var rock in Rocks)
        {
            rock.hpMultiplier = hpMultiplier;
        }
    }

    void Update()
    {
        
    }
}
