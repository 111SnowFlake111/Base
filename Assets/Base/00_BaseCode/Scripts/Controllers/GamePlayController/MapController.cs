using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Material greyColorForRoad;
    public Material goldColorForRoad;

    public GameObject conveyor;
    public GameObject roadSpawner;
    public GameObject conveyorSpawner;
    public GameObject spawner;
    public GameObject cylinder;
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
    private List<string> bonusName = new List<string> { "Fire Rate", "Range" };
    private int cylinderFirstSegment = 0;
    private int cylinderSecondSegment = 0;
    private int cylinderThirdSegment = 0;
    private void Start()
    {
        SimplePool2.Preload(cylinder, 20);
        SimplePool2.Preload(conveyor, 30);

        foreach (var obj in road)
        {
            SimplePool2.Preload(obj, 10);
        }

        for (int i = 0; i <= 205; i++)
        {
            //var obj1 = SimplePool2.Spawn(road[idLevel[i]], roadSpawner.transform.position + new Vector3(0, 0, (64.71f + 35.27198f) * i), Quaternion.identity).GetComponent<Road>();

            if (i == 50)
            {
                //Spawn Upgrade
                roadSpawning = SimplePool2.Spawn(road[3], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<UpgradeSpawner>().left = Random.Range(0, 5);
                roadSpawning.GetComponent<UpgradeSpawner>().right = Random.Range(0, 5);
                Random.Range(0, 5);
            }
            else if (i == 45)
            {
                //Spawn 2 bảng bonus
                roadSpawning = SimplePool2.Spawn(road[2], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<BonusPanelSpawner>().leftActive = true;
                roadSpawning.GetComponent<BonusPanelSpawner>().rightActive = true;

                roadSpawning.GetComponent<BonusPanelSpawner>().leftBonusName = bonusName[Random.Range(0, 2)];
                roadSpawning.GetComponent<BonusPanelSpawner>().leftAdd = Random.Range(1, 6);

                roadSpawning.GetComponent<BonusPanelSpawner>().rightBonusName = bonusName[Random.Range(0, 2)];
                roadSpawning.GetComponent<BonusPanelSpawner>().rightAdd = Random.Range(-5, 0);
                roadSpawning.GetComponent<BonusPanelSpawner>().rightTotal = 30;
            }
            else if (i % 4 == 0 && i < 65 && i > 25 && i != 50)
            {
                //FIRST SEGMENT
                //Spawn ổ đạn ở 1 trong 3 vị trí với tỷ lệ xuất hiện là 50:50, max 8 ổ
                roadSpawning = SimplePool2.Spawn(road[1], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();

                var minSpawn = 0;

                randomStatus = Random.Range(0, 2);
                if (cylinderFirstSegment <= 8)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().leftActive = (randomStatus == 1);
                    cylinderFirstSegment++;
                    minSpawn++;
                }

                randomStatus = Random.Range(0, 2);
                if (cylinderFirstSegment <= 8)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().middleActive = (randomStatus == 1);
                    cylinderFirstSegment++;
                    minSpawn++;
                }

                randomStatus = Random.Range(0, 2);
                if (cylinderFirstSegment <= 8)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().rightActive = (randomStatus == 1);
                    cylinderFirstSegment++;
                    minSpawn++;
                }

                randomStatus = Random.Range(0, 2);
                if (minSpawn < 1 && cylinderFirstSegment <= 8)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().rightActive = (randomStatus == 1);
                    cylinderFirstSegment++;
                }
            }
            else if (i == 65)
            {
                //Spawn Gate
                roadSpawning = SimplePool2.Spawn(road[4], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
            }
            else if (i == 80)
            {
                //Spawn Upgrade
                roadSpawning = SimplePool2.Spawn(road[3], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<UpgradeSpawner>().left = Random.Range(0, 5);
                roadSpawning.GetComponent<UpgradeSpawner>().right = Random.Range(0, 5);
            }
            else if (i % 4 == 0 && i < 105 && i > 65 && i != 80)
            {
                //SECOND SEGMENT
                //Spawn ổ đạn ở 1 trong 3 vị trí với tỷ lệ xuất hiện là 50:50, max 10 ổ
                roadSpawning = SimplePool2.Spawn(road[1], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();

                var minSpawn = 0;

                randomStatus = Random.Range(0, 2);
                if (cylinderSecondSegment <= 10)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().leftActive = (randomStatus == 1);
                    cylinderSecondSegment++;
                    minSpawn++;
                }

                randomStatus = Random.Range(0, 2);
                if (cylinderSecondSegment <= 10)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().middleActive = (randomStatus == 1);
                    cylinderSecondSegment++;
                    minSpawn++;
                }

                randomStatus = Random.Range(0, 2);
                if (cylinderSecondSegment <= 10)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().rightActive = (randomStatus == 1);
                    cylinderSecondSegment++;
                    minSpawn++;
                }

                //Chạy random thêm 1 lần khi không có ổ nào được spawn trên road
                randomStatus = Random.Range(0, 2);
                if (minSpawn < 1 && cylinderSecondSegment <= 10)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().rightActive = (randomStatus == 1);
                    cylinderSecondSegment++;
                }
            }
            else if (i == 105)
            {
                //Spawn Gate
                roadSpawning = SimplePool2.Spawn(road[4], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
            }
            else if (i == 115 || i == 45 || i == 155 || i == 135)
            {
                //Spawn Spike, tỷ lệ 50:50, max 2 set
                roadSpawning = SimplePool2.Spawn(road[6], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();

                var spikesLimit = 0;

                randomStatus = Random.Range(0, 2);
                if (randomStatus == 1 && spikesLimit <= 2)
                {
                    roadSpawning.GetComponent<SetOfSpikesRoadController>().leftSet = true;
                    spikesLimit++;
                }

                randomStatus = Random.Range(0, 2);
                if (randomStatus == 1 && spikesLimit <= 2)
                {
                    roadSpawning.GetComponent<SetOfSpikesRoadController>().middleSet = true;
                    spikesLimit++;
                }

                //Chạy random thêm 1 lần khi không có ổ nào được spawn trên road
                randomStatus = Random.Range(0, 2);
                if (randomStatus == 1 && spikesLimit <= 2)
                {
                    roadSpawning.GetComponent<SetOfSpikesRoadController>().rightSet = true;
                    spikesLimit++;
                }
            }
            else if (i == 130)
            {
                //Spawn 2 bảng bonus
                roadSpawning = SimplePool2.Spawn(road[2], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<BonusPanelSpawner>().leftActive = true;
                roadSpawning.GetComponent<BonusPanelSpawner>().rightActive = true;

                roadSpawning.GetComponent<BonusPanelSpawner>().leftBonusName = bonusName[Random.Range(0, 2)];
                roadSpawning.GetComponent<BonusPanelSpawner>().leftAdd = Random.Range(2, 11);

                roadSpawning.GetComponent<BonusPanelSpawner>().rightBonusName = bonusName[Random.Range(0, 2)];
                roadSpawning.GetComponent<BonusPanelSpawner>().rightAdd = Random.Range(-10, 0);
                roadSpawning.GetComponent<BonusPanelSpawner>().rightTotal = 70;
            }
            else if (i == 150)
            {
                //Spawn 2 bảng bonus
                roadSpawning = SimplePool2.Spawn(road[2], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<BonusPanelSpawner>().leftActive = true;
                roadSpawning.GetComponent<BonusPanelSpawner>().rightActive = true;

                roadSpawning.GetComponent<BonusPanelSpawner>().leftBonusName = bonusName[Random.Range(0, 2)];
                roadSpawning.GetComponent<BonusPanelSpawner>().leftAdd = Random.Range(2, 11);
                roadSpawning.GetComponent<BonusPanelSpawner>().leftTotal = -30;

                roadSpawning.GetComponent<BonusPanelSpawner>().rightBonusName = bonusName[Random.Range(0, 2)];
                roadSpawning.GetComponent<BonusPanelSpawner>().rightAdd = Random.Range(2, 11);
                roadSpawning.GetComponent<BonusPanelSpawner>().rightTotal = -30;
            }
            else if (i == 165)
            {
                //Spawn 1 bảng bonus
                roadSpawning = SimplePool2.Spawn(road[2], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                roadSpawning.GetComponent<BonusPanelSpawner>().middleActive = true;

                roadSpawning.GetComponent<BonusPanelSpawner>().middleBonusName = bonusName[Random.Range(0, 2)];
                roadSpawning.GetComponent<BonusPanelSpawner>().middleAdd = 5;
            }
            else if (i % 4 == 0 && i < 180 && i > 105 && i != 130 && i != 150)
            {
                //THIRD SEGMENT
                //Spawn ổ đạn ở 1 trong 3 vị trí với tỷ lệ xuất hiện là 50:50, max 20 ổ
                roadSpawning = SimplePool2.Spawn(road[1], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();

                var minSpawn = 0;

                randomStatus = Random.Range(0, 2);
                if (cylinderThirdSegment <= 20)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().leftActive = (randomStatus == 1);
                    cylinderThirdSegment++;
                    minSpawn++;
                }

                randomStatus = Random.Range(0, 2);
                if (cylinderThirdSegment <= 20)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().middleActive = (randomStatus == 1);
                    cylinderThirdSegment++;
                    minSpawn++;
                }

                randomStatus = Random.Range(0, 2);
                if (cylinderThirdSegment <= 20)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().rightActive = (randomStatus == 1);
                    cylinderThirdSegment++;
                    minSpawn++;
                }

                //Chạy random thêm 1 lần khi không có ổ nào được spawn trên road
                randomStatus = Random.Range(0, 2);
                if (minSpawn < 1 && cylinderThirdSegment <= 20)
                {
                    roadSpawning.GetComponent<CylinderSpawner>().rightActive = (randomStatus == 1);
                    cylinderThirdSegment++;
                }
            }
            else if (i == 180)
            {
                //Spawn LastGate
                roadSpawning = SimplePool2.Spawn(road[5], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
            }
            else if (i == 205)
            {
                //Spawn Đích
                roadSpawning = SimplePool2.Spawn(road[8], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();

                Transform subObj = roadSpawning.transform.Find("road_1");
                Renderer ren = subObj.gameObject.GetComponent<Renderer>();
                ren.material = goldColorForRoad;
            }
            else if (i >= 192 && i % 2 == 0 && i <= 200)
            {
                //Spawn rock ở cuối đường, nếu va phải thì Player sẽ die
                roadSpawning = SimplePool2.Spawn(road[7], roadSpawner.transform.position + new Vector3(0, 0, 3.4f * i), Quaternion.identity).GetComponent<Road>();
                rockCount += 7;
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

}
