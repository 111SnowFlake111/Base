using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfRoadReward : MonoBehaviour
{
    public List<GameObject> rewards;
    public GameObject pileOfCash;

    GameObject reward;
    int count;
    void Start()
    {
        if (UseProfile.OwnedSpecialGuns.Contains("Dragunov"))
        {
            count++;
            if (UseProfile.OwnedSpecialGuns.Contains("M79"))
            {
                count++;
                reward = Instantiate(pileOfCash, gameObject.transform.position + new Vector3(0, 0, -0.3f), Quaternion.identity);
            }
            else
            {
                reward = Instantiate(rewards[count], gameObject.transform.position, Quaternion.identity);
            }
        }
        else
        {
            reward = Instantiate(rewards[count], gameObject.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (count < rewards.Count)
        {
            reward.transform.Rotate(0, 2f, 0);
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (count)
            {
                case 0:
                    {
                        UseProfile.OwnedSpecialGuns += "Dragunov";
                        break;
                    }
                case 1:
                    {
                        UseProfile.OwnedSpecialGuns += "M79";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            GamePlayController.Instance.playerContain.victory = true;
            PopupEndGame.Setup().Show();
            Destroy(gameObject);
        }
    }
}
