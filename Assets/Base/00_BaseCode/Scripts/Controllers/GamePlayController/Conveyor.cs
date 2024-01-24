using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public IEnumerator ConveyorDestroyer()
    {
        yield return new WaitForSeconds(30);
        SimplePool2.Despawn(gameObject);
    }

    public IEnumerator ConveyorDelayedDespawn()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        SimplePool2.Despawn(gameObject);
    }

    public void OnTriggerEnter(UnityEngine.Collider collision)
    {
        if (collision.gameObject.tag == "LastGate")
        {
            StartCoroutine(ConveyorDelayedDespawn());
        }
    }
}
