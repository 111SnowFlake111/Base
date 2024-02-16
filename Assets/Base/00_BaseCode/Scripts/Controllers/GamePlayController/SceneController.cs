using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Text win;
    public Text lose;

    public Canvas canvas;

    private void Update()
    {
        //if (!GamePlayController.Instance.playerContain.isAlive)
        //{
        //    canvas.gameObject.SetActive(true);
        //    lose.gameObject.SetActive(true);
        //}

        //if (GamePlayController.Instance.playerContain.victory)
        //{
        //    canvas.gameObject.SetActive(true);
        //    win.gameObject.SetActive(true);
        //}
    }

    public void Restart()
    {
        canvas.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        GamePlayController.Instance.playerContain.isAlive = true;
        SceneManager.LoadScene("GamePlay");
    }

    public void ToMenu()
    {
        canvas.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
