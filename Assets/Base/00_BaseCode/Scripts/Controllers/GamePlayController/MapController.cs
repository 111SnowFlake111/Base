using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    
    public List<GameObject> testList;

    public GameObject road;
    public GameObject roadSpawner;
    private float timer = 0f;
    private void Start()
    {
        HandleSpawnRoad();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            HandleSpawnRoad();
            timer = 0f;
        }      
    }

    public void HandleSpawnRoad()
    {
        int ranIndex = Random.Range(0, testList.Count);
        Instantiate(testList[ranIndex], roadSpawner.transform.position, Quaternion.identity);
    }
}
