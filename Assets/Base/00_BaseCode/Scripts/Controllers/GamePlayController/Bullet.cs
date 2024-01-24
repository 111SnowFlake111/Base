using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bullet : MonoBehaviour
{
    static float rangeBuff;
    public float inaccuracy = 0;
    private bool spawnCheck = false;

    public Transform posBullet;

    void Update()
    {
        if (!spawnCheck)
        {
            inaccuracy = RandomX();
        }       
        posBullet.transform.position += new Vector3(inaccuracy, 0, 50f) * Time.deltaTime;
    }
    public IEnumerator HandleDestoy(float baseRange)
    {
        //Sau này khi bonus panel hoàn thành thì có thể cộng thêm thời gian, nó sẽ tương đương việc tăng Range
        yield return new WaitForSecondsRealtime(baseRange + GamePlayController.Instance.playerContain.bonusRange);
        spawnCheck = false;
        SimplePool2.Despawn(this.gameObject);
    }

    public float RandomX()
    {
        if (GamePlayController.Instance.playerContain.handController.currentGun == 1)
        {
            spawnCheck = true;
            return Random.Range(-3f, 3f);
        } 
        else if (GamePlayController.Instance.playerContain.handController.currentGun == 2)
        {
            spawnCheck = true;
            return Random.Range(-1.5f, 1.5f);
        } 
        else
        {
            spawnCheck = true;
            return 0;
        }
    }
}
