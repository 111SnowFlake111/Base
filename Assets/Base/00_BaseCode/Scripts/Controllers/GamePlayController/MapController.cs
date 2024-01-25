using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Material greyColorForRoad;

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

    private int randomStatus;
    private float timer = 0f;
    private float timer2 = 0f;
    private int rockCount = 0;
    public int roadCount = 0;
    //private int posCount = 0;

    private Road roadSpawning;
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

        for (int i = 0; i < 200; i++)
        {
            //var obj1 = SimplePool2.Spawn(road[idLevel[i]], roadSpawner.transform.position + new Vector3(0, 0, (64.71f + 35.27198f) * i), Quaternion.identity).GetComponent<Road>();

            if (i == 50)
            {
                //Spawn Upgrade
                roadSpawning = SimplePool2.Spawn(road[3], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<UpgradeSpawner>().left = 1;
                roadSpawning.GetComponent<UpgradeSpawner>().right = 3;
            }
            else if (i == 80)
            {
                //Spawn Upgrade
                roadSpawning = SimplePool2.Spawn(road[3], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<UpgradeSpawner>().left = 2;
                roadSpawning.GetComponent<UpgradeSpawner>().right = 4;
            }
            else if (i == 100)
            {
                //Spawn Gate
                roadSpawning = SimplePool2.Spawn(road[4], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
            }
            else if (i == 115)
            {
                roadSpawning = SimplePool2.Spawn(road[5], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
            }
            else if (i == 130)
            {
                //Spawn 2 bảng bonus
                roadSpawning = SimplePool2.Spawn(road[2], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<BonusPanelSpawner>().leftActive = true;
                roadSpawning.GetComponent<BonusPanelSpawner>().rightActive = true;

                roadSpawning.GetComponent<BonusPanelSpawner>().leftBonusName = "Fire Rate";
                roadSpawning.GetComponent<BonusPanelSpawner>().leftAdd = +2;

                roadSpawning.GetComponent<BonusPanelSpawner>().rightBonusName = "Fire Rate";
                roadSpawning.GetComponent<BonusPanelSpawner>().rightAdd = -5;
                roadSpawning.GetComponent<BonusPanelSpawner>().rightTotal = 50;
            }
            else if (i == 160)
            {
                //Spawn 1 bảng bonus
                roadSpawning = SimplePool2.Spawn(road[2], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<BonusPanelSpawner>().middleActive = true;

                roadSpawning.GetComponent<BonusPanelSpawner>().middleBonusName = "Range";
                roadSpawning.GetComponent<BonusPanelSpawner>().middleAdd = +5;
            }
            else if (i % 10 == 0 && i < 190 && i > 30)
            {
                //Spawn ổ đạn ở 1 trong 3 vị trí với tỷ lệ xuất hiện là 50:50
                roadSpawning = SimplePool2.Spawn(road[1], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();

                randomStatus = Random.Range(0, 2);
                roadSpawning.GetComponent<CylinderSpawner>().leftActive = (randomStatus == 1);

                randomStatus = Random.Range(0, 2);
                roadSpawning.GetComponent<CylinderSpawner>().middleActive = (randomStatus == 1);

                randomStatus = Random.Range(0, 2);
                roadSpawning.GetComponent<CylinderSpawner>().rightActive = (randomStatus == 1);
            }
            else if (i >= 192 && i % 2 == 0)
            {
                //Spawn rock ở cuối đường, nếu va phải thì Player sẽ die
                roadSpawning = SimplePool2.Spawn(road[6], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                rockCount += 5;
                roadSpawning.GetComponent<RockSpawner>().rockHpMultipler = rockCount;
            }
            else
            {
                //Nếu không thỏa mãn bất cứ điều kiện nào ở trên thì spawn một đường bình thường
                roadSpawning = SimplePool2.Spawn(road[0], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
            }

            if (roadCount % 2 == 0)
            {
                Transform subObj = roadSpawning.transform.Find("road_1");
                Renderer ren = subObj.gameObject.GetComponent<Renderer>();
                ren.material = greyColorForRoad;
            }
            roadCount++;
        }

        for (int j = 0; j < 30; j++)
        {
            var con = SimplePool2.Spawn(conveyor, conveyorSpawner.transform.position + new Vector3(0, 0, (32.48f - 27.481f) * j), Quaternion.identity).GetComponent<Conveyor>();
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
