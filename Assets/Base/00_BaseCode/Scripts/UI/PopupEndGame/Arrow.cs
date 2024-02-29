using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int multipler = 1;
    public List<Transform> rewardMultiplers;

    private void Update()
    {
        if (gameObject.transform.position.x <= rewardMultiplers[0].position.x || gameObject.transform.position.x >= rewardMultiplers[5].position.x)
        {
            multipler = 1;
        }

        if ((gameObject.transform.position.x >= rewardMultiplers[0].position.x && gameObject.transform.position.x <= rewardMultiplers[1].position.x) || (gameObject.transform.position.x >= rewardMultiplers[3].position.x && gameObject.transform.position.x <= rewardMultiplers[4].position.x))
        {
            multipler = 2;
        }

        if ((gameObject.transform.position.x >= rewardMultiplers[2].position.x && gameObject.transform.position.x <= rewardMultiplers[3].position.x) || (gameObject.transform.position.x >= rewardMultiplers[4].position.x && gameObject.transform.position.x <= rewardMultiplers[5].position.x))
        {
            multipler = 3;
        }
        if (gameObject.transform.position.x >= rewardMultiplers[1].position.x && gameObject.transform.position.x <= rewardMultiplers[2].position.x)
        {
            multipler = 4;
        }
    }
}
