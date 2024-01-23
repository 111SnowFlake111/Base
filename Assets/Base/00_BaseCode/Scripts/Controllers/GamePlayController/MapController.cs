using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject conveyor;
    public GameObject roadSpawner;
    public GameObject conveyorSpawner;
    public GameObject spawner;
    public GameObject bonusPanel;
    public GameObject cylinder;
    public GameObject gateBonus;
    public GameObject onConveyorPos;

    //public LineRenderer conveyerLine;

    public Transform leftLimit;
    public Transform rightLimit;

    public List<GameObject> road;
    public List<int> idLevel;
    //public List<int> gateIndex;

    private float timer = 0f;
    private float timer2 = 0f;
    //private int posCount = 0;
    public Gate gate;
    private bool gateSpawned = false;
    private void Start()
    {

        SimplePool2.Preload(cylinder, 20);
        SimplePool2.Preload(bonusPanel, 10);
        SimplePool2.Preload(conveyor, 30);

        HandleSpawnConveyor();

        foreach (var obj in road)
        {
            SimplePool2.Preload(obj, 10);
        }

        for (int i = 0; i < idLevel.Count; i++)
        {
            var obj1 = SimplePool2.Spawn(road[idLevel[i]], roadSpawner.transform.position + new Vector3(0, 0, (64.71f + 35.27198f) * i), Quaternion.identity).GetComponent<Road>();     
        }
        for (int i = 0; i < 30; i++)
        {
            var con = SimplePool2.Spawn(conveyor, conveyorSpawner.transform.position + new Vector3(0, 0, (32.48f - 27.481f) * i), Quaternion.identity).GetComponent<Conveyor>();
            con.transform.localEulerAngles = new Vector3(0, 90, 0);
            //StartCoroutine(con.ConveyorDestroyer());
        }
    }

    void Update()
    {
        if (GamePlayController.Instance.playerContain.isAlive)
        {
            if (timer2 <= 14f)
            {
                timer += Time.deltaTime;
                timer2 += Time.deltaTime;
                if (timer >= 2f)
                {
                    //StartCoroutine(HandleSpawnCylinder());
                    //StartCoroutine(HandleSpawnBonusPanel());
                    //HandleSpawnConveyor();
                    timer = 0f;
                }
                //if (timer2 >= 13f && !gateSpawned)
                //{
                //    HandleSpawnGate();
                //    gateSpawned = true;
                //}
            }
        }
    }

    public IEnumerator HandleSpawnBonusPanel()
    {
        yield return new WaitForSeconds(Mathf.RoundToInt(Random.Range(1, 5)));
        float ranXPos = Random.Range(leftLimit.position.x + 0.03f, rightLimit.position.x - 0.03f);
        var pan = SimplePool2.Spawn(bonusPanel, new Vector3(ranXPos, spawner.transform.position.y, spawner.transform.position.z), Quaternion.identity).GetComponent<BonusPanel>();
        StartCoroutine(pan.PanelDestroyer());
    }

    public IEnumerator HandleSpawnCylinder()
    {
        yield return new WaitForSeconds(Mathf.RoundToInt(Random.Range(1, 3)));
        float ranXPos = Random.Range(leftLimit.position.x + 0.03f, rightLimit.position.x - 0.03f);
        var cy = SimplePool2.Spawn(cylinder, new Vector3(ranXPos, spawner.transform.position.y + 0.5f, spawner.transform.position.z), Quaternion.identity).GetComponent<Cylinder>();
        StartCoroutine(cy.CylinderDestroyer());
    }

    public void HandleSpawnConveyor()
    {
        var con = SimplePool2.Spawn(conveyor, conveyorSpawner.transform.position, Quaternion.identity).GetComponent<Conveyor>();
        con.transform.localEulerAngles = new Vector3(0, 90, 0);
        StartCoroutine(con.ConveyorDestroyer());
    }

    public void HandleSpawnGate()
    {
        var gat = SimplePool2.Spawn(gate,spawner.transform.position, Quaternion.identity).GetComponent<Gate>();
        StartCoroutine(gat.GateDestroyer());
    }
}
