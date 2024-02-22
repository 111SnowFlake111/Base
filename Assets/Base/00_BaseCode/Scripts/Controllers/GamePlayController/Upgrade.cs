using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject upgrade;
    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.transform.Rotate(0, 2f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "UpgradePistol":
                    GamePlayController.Instance.playerContain.handController.GunUpdate(0);
                    break;
                case "UpgradeSMG":
                    GamePlayController.Instance.playerContain.handController.GunUpdate(1);
                    break;
                case "UpgradeRifle":
                    GamePlayController.Instance.playerContain.handController.GunUpdate(2);
                    break;
                case "UpgradeShotgun":
                    GamePlayController.Instance.playerContain.handController.GunUpdate(3);
                    break;
                case "UpgradeSniper":
                    GamePlayController.Instance.playerContain.handController.GunUpdate(4);
                    break;
                case "DualWield":
                    GamePlayController.Instance.playerContain.doubleGun = true;
                    GamePlayController.Instance.playerContain.tripleGun = false;
                    GamePlayController.Instance.playerContain.handController.GunUpdate(GamePlayController.Instance.playerContain.currentGun);
                    break;
                case "TripleWield":
                    GamePlayController.Instance.playerContain.doubleGun = false;
                    GamePlayController.Instance.playerContain.tripleGun = true;
                    GamePlayController.Instance.playerContain.handController.GunUpdate(GamePlayController.Instance.playerContain.currentGun);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
