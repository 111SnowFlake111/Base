using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOfSpikesRoadController : MonoBehaviour
{
    public List<GameObject> setOfSpikes;

    public bool leftSet = false;
    public bool middleSet = false;
    public bool rightSet = false;
    void Start()
    {
        setOfSpikes[0].SetActive(leftSet);
        setOfSpikes[1].SetActive(middleSet);
        setOfSpikes[2].SetActive(rightSet);
    }
}
