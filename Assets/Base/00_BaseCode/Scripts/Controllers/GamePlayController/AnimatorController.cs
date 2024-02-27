using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GamePlayController.Instance.playerContain.isAlive)
        {
            animator.SetTrigger("Death");
        }

        if (GamePlayController.Instance.playerContain.victory)
        {
            animator.SetTrigger("Victory");
        }

        if (GamePlayController.Instance.playerContain.start)
        {
            animator.SetTrigger("GameStart");
        }
    }
}
