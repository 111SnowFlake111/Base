using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfRoadReward : MonoBehaviour
{
    public List<GameObject> rewards;
    public GameObject moneyBag;

    public Transform moneyBagPos;

    List<string> specialGunNames = new List<string> {"Dragunov", "M79", "PKM", "RPG7", "M240", "M72"};
    GameObject reward;
    int count;
    void Start()
    {
        for (int i = 0; i < specialGunNames.Count; i++)
        {
            if (UseProfile.OwnedSpecialGuns.Contains(specialGunNames[i]))
            {
                count++;
            }
            else
            {
                reward = Instantiate(rewards[count], gameObject.transform.position, Quaternion.identity);
                break;
            }
        }

        if (count >= specialGunNames.Count)
        {
            reward = Instantiate(moneyBag, moneyBagPos.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        reward.transform.Rotate(0, 2f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (count)
            {
                case 0:
                    {
                        UseProfile.OwnedSpecialGuns += specialGunNames[count];
                        break;
                    }
                case 1:
                    {
                        UseProfile.OwnedSpecialGuns += specialGunNames[count];
                        break;
                    }
                case 2:
                    {
                        UseProfile.OwnedSpecialGuns += specialGunNames[count];
                        break;
                    }
                case 3:
                    {
                        UseProfile.OwnedSpecialGuns += specialGunNames[count];
                        break;
                    }
                case 4:
                    {
                        UseProfile.OwnedSpecialGuns += specialGunNames[count];
                        break;
                    }
                case 5:
                    {
                        UseProfile.OwnedSpecialGuns += specialGunNames[count];
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            GamePlayController.Instance.playerContain.victory = true;
            GamePlayController.Instance.playerContain.isMoving = false;
            GamePlayController.Instance.playerContain.start = false;
            PopupEndGame.Setup().Show();
            Destroy(gameObject);
        }
    }
}
