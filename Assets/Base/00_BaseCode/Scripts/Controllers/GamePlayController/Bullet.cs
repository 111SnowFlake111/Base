using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bullet : MonoBehaviour
{
    static float rangeBuff;
    public float inaccuracy = 0;

    public Transform posBullet;

    // Update is called once per frame
    void Update()
    {   
        if (GamePlayController.Instance.playerContain.handController.currentGun == 1)
        {
            inaccuracy = Random.Range(-10f, 10f);
        }

        if (GamePlayController.Instance.playerContain.handController.currentGun == 2)
        {
            inaccuracy = Random.Range(-5f, 5f);
        }
        posBullet.transform.position += new Vector3(inaccuracy, 0, 40f) * Time.deltaTime;
    }
    public IEnumerator HandleDestoy(float baseRange)
    {
        //Sau này khi bonus panel hoàn thành thì có thể cộng thêm thời gian, nó sẽ tương đương việc tăng Range
        yield return new WaitForSecondsRealtime(baseRange);
        SimplePool2.Despawn(this.gameObject); 
    }
}
